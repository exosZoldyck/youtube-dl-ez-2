using System.Diagnostics;
using System.Text.Json;

namespace youtube_dl_ez_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            thumbnailPreviewBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        int buttonState = 1;
        int selectedQuality = 0;
        int tryCount = 0;
        List<String> availableQ = new List<String>();
        bool debugMode;
        bool writeLogFile;
        bool showProgress;
        bool downloadSubtitles;
        bool noThumbnail;
        bool noThumbnailDownload;
        bool autoBestQuality;
        bool disableEnter = false;
        bool subDownloaded = false;
        bool isAudioOnlyDownload;
        bool doNotStartDownload = false; // For fixing autoselection multi yt-dlp instances start error
        Image thumbnailPreviewImage;
        String ytURL = "";
        String finalFileDir = Path.GetFullPath("./Downloads/");
        String lastVideoDownloadedPath = null;
        String fileNameLatestCreationTime = "";
        const String locationNameTemplate = "\"./temp/%(title)s.%(ext)s\"";
        String uploadDate;
        String uploaderName;
        const String configFile = "config.json";
        const String downloadPathFile = "downloadDir.cfg";
        Dictionary<String, bool> configValues;

        // Adding to config file:
        //  - Form1_Load
        //  - Event Listener
        //  - resetDefaultConfigToolStripMenuItem_Click()
        //  - writeDefaultConfigFile() 
        //  - setAllButtonsEnabled()

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(configFile)) writeDefaultConfigFile();
            if (!File.Exists(downloadPathFile)) File.WriteAllText(downloadPathFile, Path.GetFullPath("./Downloads/"));

            String configJson = File.ReadAllText(configFile);
            configValues = JsonSerializer.Deserialize<Dictionary<String, bool>>(configJson);

            autoBestQuality = configValues["autoBestQuality"];
            downloadSubtitles = configValues["downloadSubtitles"];
            writeLogFile = configValues["writeLogFile"];
            debugMode = configValues["debugMode"];
            showProgress = configValues["showProgress"];
            noThumbnailDownload = configValues["noThumbnailDownload"];

            finalFileDir = File.ReadAllText(downloadPathFile);

            autoBestQualityToolStripMenuItem.Checked = autoBestQuality;
            subtitlesToolStripMenuItem.Checked = downloadSubtitles;
            writeLogsToolStripMenuItem.Checked = writeLogFile;
            showProgressToolStripMenuItem.Checked = showProgress;
            noThumbnailToolStripMenuItem.Checked = noThumbnailDownload;

            consoleOutputTextbox.ScrollBars = ScrollBars.None;
            consoleOutputTextbox.TextChanged += consoleOutputTextbox_TextChanged;
            Control.CheckForIllegalCrossThreadCalls = false; // DANGEROUS!!!

            urlTextBox.Select();
        }

        private void consoleOutputTextbox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int visibleLines = textBox.ClientSize.Height / textBox.Font.Height;
            textBox.ScrollBars = visibleLines < textBox.Lines.Length ? ScrollBars.Vertical : ScrollBars.None;
        }

        private void videoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            buttonState = 1;
        }

        private void audioRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            buttonState = 2;
        }

        private void autoBestQualityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoBestQuality = autoBestQualityToolStripMenuItem.Checked;
            configValues["autoBestQuality"] = autoBestQuality;
            writeToConfigFile();
        }

        private void downloadSubtitlesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            downloadSubtitles = subtitlesToolStripMenuItem.Checked;
            configValues["downloadSubtitles"] = downloadSubtitles;
            writeToConfigFile();
        }

        private void writeLogTextCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            writeLogFile = writeLogsToolStripMenuItem.Checked;
            configValues["writeLogFile"] = writeLogFile;
            writeToConfigFile();
        }

        private void showProgressCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            showProgress = showProgressToolStripMenuItem.Checked;
            configValues["showProgress"] = showProgress;
            writeToConfigFile();
        }

        private void noThumbnailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noThumbnailDownload = noThumbnailToolStripMenuItem.Checked;
            configValues["noThumbnailDownload"] = noThumbnailDownload;
            writeToConfigFile();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consoleOutputTextbox.Text = "";
            thumbnailPreviewBox.Image = null;
            videoRadioButton.Checked = true;
            restart();
        }

        private void chooseDownloadDirButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !String.IsNullOrWhiteSpace(dialog.SelectedPath)) finalFileDir = dialog.SelectedPath + "\\";
                File.WriteAllText(downloadPathFile, finalFileDir);
                consoleOutputTextbox.Text = $"Download folder changed to \"{finalFileDir}\"";
            }
        }

        private void restoreDefaultDownloadsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restoreDownloadDir(false);
            consoleOutputTextbox.Text = $"Download folder restored to \"{finalFileDir}\"";
        }

        private void openDownloadDirButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("./Downloads/") && finalFileDir == Path.GetFullPath("./Downloads/")) Directory.CreateDirectory("./Downloads/");
            Process.Start("explorer.exe", finalFileDir);
        }

        private void checkForYtdlpUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String downloadOnlyVideo_arg = $"--update";
            Process downloadOnlyVideo = new Process();
            downloadOnlyVideo.StartInfo.FileName = @".\yt-dlp";
            downloadOnlyVideo.StartInfo.Arguments = downloadOnlyVideo_arg;
            downloadOnlyVideo.StartInfo.UseShellExecute = false;
            downloadOnlyVideo.StartInfo.CreateNoWindow = false;
            downloadOnlyVideo.Start();
            downloadOnlyVideo.WaitForExit();

            foreach (String file in Directory.EnumerateFiles("./", "*.exe.old*"))
            {
                MessageBox.Show("Update successful!");
                File.Delete(file);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resetDefaultConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want restore to default settings?", "Confirm", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try { File.Delete("config.json"); }
                catch { };

                writeDefaultConfigFile();

                String configJson = File.ReadAllText(configFile);
                configValues = JsonSerializer.Deserialize<Dictionary<String, bool>>(configJson);

                autoBestQuality = configValues["autoBestQuality"];
                downloadSubtitles = configValues["downloadSubtitles"];
                writeLogFile = configValues["writeLogFile"];
                debugMode = configValues["debugMode"];
                showProgress = configValues["showProgress"];
                noThumbnailDownload = configValues["noThumbnailDownload"];

                autoBestQualityToolStripMenuItem.Checked = autoBestQuality;
                subtitlesToolStripMenuItem.Checked = downloadSubtitles;
                writeLogsToolStripMenuItem.Checked = writeLogFile;
                showProgressToolStripMenuItem.Checked = showProgress;
                noThumbnailToolStripMenuItem.Checked = noThumbnail;

                restoreDownloadDir(false);

                try { 
                    thumbnailPreviewImage.Dispose();
                    thumbnailPreviewBox.Image = null;
                }
                catch (Exception ex) { Console.Out.Write(ex.ToString); }

                consoleOutputTextbox.Text = "Default settings have been restored!";
            }
            else return;
        }

        private void playVideoButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(lastVideoDownloadedPath))
            {
                try {
                    Process.Start(lastVideoDownloadedPath);
                    consoleOutputTextbox.AppendText(Environment.NewLine);
                    consoleOutputTextbox.Text += $"Now playing {lastVideoDownloadedPath}!";
                }
                catch {
                    try {
                        Process.Start(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe", lastVideoDownloadedPath);
                        consoleOutputTextbox.AppendText(Environment.NewLine);
                        consoleOutputTextbox.Text += $"Now playing {lastVideoDownloadedPath}!";
                    }
                    catch {
                        consoleOutputTextbox.AppendText(Environment.NewLine);
                        consoleOutputTextbox.Text += $"ERROR: Unable to play video!";
                    }
                }
            }
            else {
                consoleOutputTextbox.AppendText(Environment.NewLine);
                consoleOutputTextbox.Text += $"ERROR: Unable to play video!";
            }
        }

        private void startDownloadButton_Click(object sender, EventArgs e)
        {
            consoleOutputTextbox.Text = "";
            try
            {
                ytURL = urlTextBox.Text;
            }
            catch
            {
                consoleOutputTextbox.Text += "Unable to read URL!";
                return;
            }

            if (buttonState == 0) return;
            if (ytURL == null || ytURL == "") return; 

            thumbnailPreviewBox.Image = null;
            consoleOutputTextbox.Text = "Loading available video qualities...";
            consoleOutputTextbox.AppendText(Environment.NewLine);

            switch (buttonState)
            {
                case 1:
                    thumbnailPreviewBox.Image = null;
                    isAudioOnlyDownload = false;
                    standardVideoDownload_qualityRead();
                    break;
                case 2:
                    thumbnailPreviewBox.Image = null;
                    isAudioOnlyDownload = true;
                    metadataDownload();
                    break;
                default:
                    return;
            }

            return;
        }

        private void urlTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (disableEnter) return;

            if (e.KeyChar == (char)Keys.Enter)
            {
                startDownloadButton_Click(sender, e);
            }
        }

        public void standardVideoDownload_qualityRead()
        {
            setAllButtonsEnabled(false);
            disableEnter = true;
            qualitySelectListBox.Enabled = true;
            newToolStripMenuItem.Enabled = true;

            if(!ytURL.Contains("youtube.com") && !ytURL.Contains("youtu.be"))
            {
                consoleOutputTextbox.Text = ("ERROR: Unsupported URL!");
                restart();
                return;
            }

            String qualityDownload_arg = $"-F {ytURL} --skip-download --no-playlist";
            Process qualityDownload = new Process();
            qualityDownload.StartInfo.FileName = @".\yt-dlp";
            qualityDownload.StartInfo.Arguments = qualityDownload_arg;
            qualityDownload.StartInfo.RedirectStandardOutput = true;
            qualityDownload.StartInfo.RedirectStandardError = true;
            qualityDownload.StartInfo.UseShellExecute = false;
            qualityDownload.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            qualityDownload.StartInfo.CreateNoWindow = true;
            String eOut = null;
            String stdout = "";
            qualityDownload.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => { eOut += e.Data; });
            qualityDownload.EnableRaisingEvents = true;
            qualityDownload.Exited += (s, args) => 
            {
                availableQ.Clear();
                if (stdout.Replace(" ", "").Contains("337webm")) availableQ.Add("337"); //4k60
                else if (stdout.Replace(" ", "").Contains("315webm")) availableQ.Add("315");
                else if (stdout.Replace(" ", "").Contains("313webm")) availableQ.Add("313"); // 4k

                if (stdout.Replace(" ", "").Contains("336webm")) availableQ.Add("336"); // 2k60
                else if (stdout.Replace(" ", "").Contains("308webm")) availableQ.Add("308");
                else if (stdout.Replace(" ", "").Contains("271webm")) availableQ.Add("271"); // 2k

                if (stdout.Replace(" ", "").Contains("299mp4")) availableQ.Add("299"); // 1080p60
                else if (stdout.Replace(" ", "").Contains("137mp4")) availableQ.Add("137"); // 1080p

                if (stdout.Replace(" ", "").Contains("298mp4")) availableQ.Add("298"); // 720p60
                else if (stdout.Replace(" ", "").Contains("136mp4")) availableQ.Add("136"); // 720p

                if (stdout.Replace(" ", "").Contains("135mp4")) availableQ.Add("135"); // 480p
                if (stdout.Replace(" ", "").Contains("134mp4")) availableQ.Add("134"); // 360p
                if (stdout.Replace(" ", "").Contains("133mp4")) availableQ.Add("133"); // 240p
                if (stdout.Replace(" ", "").Contains("160mp4")) availableQ.Add("160"); // 144p

                if (availableQ.Count == 0)
                {
                    consoleOutputTextbox.Text = ("ERROR: Video unavailable!");
                    tryCount++;
                    if (tryCount >= 3) MessageBox.Show("Tip: If you are getting constant errors, try to check for an update!");
                    restart();
                    return;
                }

                try
                {
                    foreach (String Q in availableQ)
                    {
                        switch (Q)
                        {
                            case "337":
                                qualitySelectListBox.Items.Add($"2160p60");
                                break;
                            case "315":
                                qualitySelectListBox.Items.Add($"2160p60");
                                break;
                            case "336":
                                qualitySelectListBox.Items.Add($"1440p60");
                                break;
                            case "308":
                                qualitySelectListBox.Items.Add($"1440p60");
                                break;
                            case "313":
                                qualitySelectListBox.Items.Add($"2160p");
                                break;
                            case "271":
                                qualitySelectListBox.Items.Add("1440p");
                                break;
                            case "299":
                                qualitySelectListBox.Items.Add("1080p60");
                                break;
                            case "298":
                                qualitySelectListBox.Items.Add("720p60");
                                break;
                            case "137":
                                qualitySelectListBox.Items.Add("1080p");
                                break;
                            case "136":
                                qualitySelectListBox.Items.Add("720p");
                                break;
                            case "135":
                                qualitySelectListBox.Items.Add("480p");
                                break;
                            case "134":
                                qualitySelectListBox.Items.Add("360p");
                                break;
                            case "133":
                                qualitySelectListBox.Items.Add("240p");
                                break;
                            case "160":
                                qualitySelectListBox.Items.Add("144p");
                                break;
                            default:
                                consoleOutputTextbox.Text += "ERROR: Invalid quality value processed!";
                                return;
                        }

                        if (autoBestQuality && availableQ.Count > 0)
                        {
                            qualitySelectListBox.SelectedIndex = 0;
                        }
                        else consoleOutputTextbox.Text = "Please select a video quality.";
                    }
                }
                catch { return; }
            };
            qualityDownload.Start();
            qualityDownload.BeginErrorReadLine();
            stdout = qualityDownload.StandardOutput.ReadToEnd();
        }

        private void qualitySelectListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                selectedQuality = Int32.Parse(availableQ[qualitySelectListBox.SelectedIndex]);
            }
            catch { return; }

            if (!doNotStartDownload) metadataDownload();
        }

        public void metadataDownload()
        {
            // Downloads the video metadata, creates the "temp" dir in .exe root dir

            doNotStartDownload = true;
            qualitySelectListBox.Enabled = false;
            newToolStripMenuItem.Enabled = false;

            consoleOutputTextbox.Text = "Starting download...";
            consoleOutputTextbox.AppendText(Environment.NewLine);
            consoleOutputTextbox.Text += $"Download folder path: {finalFileDir}";
            consoleOutputTextbox.AppendText(Environment.NewLine);
            if (!isAudioOnlyDownload)
            {
                if (!autoBestQuality) consoleOutputTextbox.Text += $"Video quality: {qualitySelectListBox.Items[qualitySelectListBox.SelectedIndex].ToString()}";
                else consoleOutputTextbox.Text += $"Video quality: {qualitySelectListBox.Items[0].ToString()}";
            }
            consoleOutputTextbox.AppendText(Environment.NewLine);
            if (!noThumbnailDownload) consoleOutputTextbox.Text += $"Download thumbnail: True";
            else consoleOutputTextbox.Text += $"Download thumbnail: False";
            consoleOutputTextbox.AppendText(Environment.NewLine);
            if (downloadSubtitles) consoleOutputTextbox.Text += $"Download subtitles: True";
            else consoleOutputTextbox.Text += $"Download subtitles: False";
            consoleOutputTextbox.AppendText(Environment.NewLine);

            qualitySelectListBox.Items.Clear();
            Directory.CreateDirectory(@".\temp\");

            consoleOutputTextbox.AppendText(Environment.NewLine);
            consoleOutputTextbox.Text += "Downloading video metadata...";
            consoleOutputTextbox.AppendText(Environment.NewLine);
            String thumbnail_arg = "";
            if (!noThumbnailDownload) thumbnail_arg = "--write-thumbnail ";
            String downloadOnlyVideo_arg = $"{ytURL} -o {locationNameTemplate} --no-playlist -r 1000M -R 10 --skip-download --write-description {thumbnail_arg}--write-info-json";
            Process downloadOnlyVideo = new Process();
            downloadOnlyVideo.StartInfo.FileName = @".\yt-dlp";
            downloadOnlyVideo.StartInfo.Arguments = downloadOnlyVideo_arg;
            downloadOnlyVideo.StartInfo.UseShellExecute = false;
            if (!showProgress) downloadOnlyVideo.StartInfo.CreateNoWindow = true;
            downloadOnlyVideo.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            downloadOnlyVideo.EnableRaisingEvents = true;
            downloadOnlyVideo.Exited += (s, args) => processMetadata();
            downloadOnlyVideo.Start();
        }

        public void processMetadata()
        {
            // Finds video file name, parses uploader name, and upload date from .json file, does thumbnail file conversion

            var files = Directory.EnumerateFiles("./temp/", "*.json");
            DateTime latestCreationTime = new DateTime(2000, 1, 1);
            fileNameLatestCreationTime = "";
            try
            {
                foreach (String file in files)
                {
                    DateTime currentCreationTime = File.GetCreationTime(file);
                    if (currentCreationTime > latestCreationTime)
                    {
                        latestCreationTime = currentCreationTime;
                        fileNameLatestCreationTime = file;
                    }
                }
            }
            catch { }
            try
            {
                fileNameLatestCreationTime = fileNameLatestCreationTime.Substring(7);
                fileNameLatestCreationTime = fileNameLatestCreationTime.Substring(0, fileNameLatestCreationTime.Length - 10);
            }
            catch
            {
                consoleOutputTextbox.Text += "ERROR: Unable to download/process video metadata!";
                restart();
                return;
            }

            String rawMetadataJson = File.ReadAllText($"./temp/{fileNameLatestCreationTime}.info.json");
            Dictionary<String, object> metadataJson = JsonSerializer.Deserialize<Dictionary<String, object>>(rawMetadataJson);
            uploadDate = metadataJson["upload_date"].ToString();
            uploadDate = uploadDate.Insert(4, "-");
            uploadDate = uploadDate.Insert(7, "-");
            uploadDate = uploadDate.Insert(uploadDate.Length, " 00:00:00");
            uploaderName = metadataJson["channel"].ToString();

            if (!noThumbnailDownload)
            {
                if (File.Exists($"./temp/{fileNameLatestCreationTime}.jpg"))
                {
                    File.Copy($"./temp/{fileNameLatestCreationTime}.jpg", $"./temp/{fileNameLatestCreationTime}.webp", true);
                    try
                    {
                        File.Delete($"./temp/{fileNameLatestCreationTime}.jpg");
                    }
                    catch { }
                }

                noThumbnail = false;
                String covertWEBPtoJPG_arg = $"-format JPG ./temp/*.webp";
                Process covertWEBPtoJPG = new Process();
                covertWEBPtoJPG.StartInfo.FileName = @".\mogrify";
                covertWEBPtoJPG.StartInfo.Arguments = covertWEBPtoJPG_arg;
                if (!showProgress) covertWEBPtoJPG.StartInfo.CreateNoWindow = true;
                if (File.Exists($"./temp/{fileNameLatestCreationTime}.jpg") || File.Exists($"./temp/{fileNameLatestCreationTime}.webp"))
                {
                    covertWEBPtoJPG.EnableRaisingEvents = true;
                    covertWEBPtoJPG.Exited += (s, args) =>
                    {
                        try
                        {
                            thumbnailPreviewImage = Image.FromFile($"./temp/{fileNameLatestCreationTime}.jpg");
                            thumbnailPreviewBox.Image = thumbnailPreviewImage;
                        }
                        catch { }

                        processMetadata_postProcess();
                    };
                    covertWEBPtoJPG.Start();
                }
                else
                {
                    consoleOutputTextbox.Text += "WARNING: Unable to download/process thumbnail!";
                    noThumbnail = true;

                    processMetadata_postProcess();
                }
            }

            else processMetadata_postProcess();
        }

        private void processMetadata_postProcess()
        {
            if (File.Exists($"./temp/{fileNameLatestCreationTime}.en.vtt")) subDownloaded = true;
            if (subDownloaded)
            {
                consoleOutputTextbox.Text += "Subtitles downloaded!";
                consoleOutputTextbox.AppendText(Environment.NewLine);
            }


            consoleOutputTextbox.Text += "Metadata download complete!";
            consoleOutputTextbox.AppendText(Environment.NewLine);

            if (!isAudioOnlyDownload) startVideoDownload();
            else startAudioDownload();
        }

        public void startVideoDownload() {
            // Downloads .mp4 file

            consoleOutputTextbox.AppendText(Environment.NewLine);
            consoleOutputTextbox.Text += "\nDownloading video...";
            consoleOutputTextbox.AppendText(Environment.NewLine);
            String subtitles_arg = "";
            if (downloadSubtitles) subtitles_arg = " --write-sub --sub-langs all --embed-subs";
            String downloadOnlyVideo_arg = $"{ytURL} -f {selectedQuality} -o {locationNameTemplate} --no-playlist -r 1000M -R 10 --add-metadata --embed-chapters{subtitles_arg}";
            Process downloadVideo = new Process();
            downloadVideo.StartInfo.FileName = @".\yt-dlp";
            downloadVideo.StartInfo.Arguments = downloadOnlyVideo_arg;
            downloadVideo.StartInfo.UseShellExecute = false;
            if (!showProgress) downloadVideo.StartInfo.CreateNoWindow = true;
            downloadVideo.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            downloadVideo.EnableRaisingEvents = true;
            downloadVideo.Exited += (s, args) => processVideo();
            downloadVideo.Start();
        }

        public void processVideo() {
            // Checks if .mp4 file was successfully downloaded, converts video to .mp4 if the file format is .webp

            if (!File.Exists($"./temp/{fileNameLatestCreationTime}.mp4"))
            {
                //MessageBox.Show("No .mp4 file found!");

                if (!File.Exists($"./temp/{fileNameLatestCreationTime}.webm"))
                {
                    consoleOutputTextbox.Text += "ERROR: Unable to download video!";
                    restart();
                    return;
                }

                Process convertWEBMToMP4 = new Process();
                convertWEBMToMP4.StartInfo.FileName = @".\ffmpeg";
                convertWEBMToMP4.StartInfo.Arguments = $"-i \"./temp/{fileNameLatestCreationTime}.webm\" -map 0:v -map 0:s? -c:v copy -c:s mov_text -f mp4 \"./temp/{fileNameLatestCreationTime}.mp4\"";
                convertWEBMToMP4.StartInfo.UseShellExecute = false;
                if (!showProgress) convertWEBMToMP4.StartInfo.CreateNoWindow = true;
                convertWEBMToMP4.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                convertWEBMToMP4.EnableRaisingEvents = true;
                convertWEBMToMP4.Exited += (s, args) =>
                {
                    File.Delete($"./temp/{fileNameLatestCreationTime}.webm");

                    if (!File.Exists($"./temp/{fileNameLatestCreationTime}.mp4"))
                    {
                        consoleOutputTextbox.Text += "ERROR: Unable to process file!";
                        restart();
                        return;
                    }

                    consoleOutputTextbox.Text += "Video download complete!";
                    consoleOutputTextbox.AppendText(Environment.NewLine);

                    if (Directory.EnumerateFiles("./temp/", "*.vtt").Count() > 0 && downloadSubtitles) subDownloaded = true;
                    if (subDownloaded)
                    {
                        consoleOutputTextbox.Text += "Subtitles downloaded!";
                        consoleOutputTextbox.AppendText(Environment.NewLine);
                    }

                    startAudioDownload();
                };
                convertWEBMToMP4.Start();
            }
            else
            {
                consoleOutputTextbox.Text += "Video download complete!";
                consoleOutputTextbox.AppendText(Environment.NewLine);

                if (Directory.EnumerateFiles("./temp/", "*.vtt").Count() > 0 && downloadSubtitles) subDownloaded = true;
                if (subDownloaded)
                {
                    consoleOutputTextbox.Text += "Subtitles downloaded!";
                    consoleOutputTextbox.AppendText(Environment.NewLine);
                }

                startAudioDownload();
            }
        }

        public void startAudioDownload() {
            // Download .mp3 file

            consoleOutputTextbox.AppendText(Environment.NewLine);
            consoleOutputTextbox.Text += "Downloading audio...";
            consoleOutputTextbox.AppendText(Environment.NewLine);
            String audioDownload_arg = $"{ytURL} -o {locationNameTemplate} -x --audio-format mp3 --audio-quality 0 --no-playlist -r 1000M -R 10 --add-metadata";
            Process audioDownload = new Process();
            audioDownload.StartInfo.FileName = @".\yt-dlp";
            audioDownload.StartInfo.Arguments = audioDownload_arg;
            if (!showProgress) audioDownload.StartInfo.CreateNoWindow = true;
            audioDownload.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            audioDownload.EnableRaisingEvents = true;
            audioDownload.Exited += (s, args) => processAudio();
            audioDownload.Start();
        }

        public void processAudio() {
            // Checks if .mp3 file was successfully downloaded

            if (!File.Exists($"./temp/{fileNameLatestCreationTime}.mp3"))
            {
                consoleOutputTextbox.Text += "ERROR: Unable to download/process .mp3 file!";
                restart();
                return;
            }

            consoleOutputTextbox.Text += "Audio download complete!";
            consoleOutputTextbox.AppendText(Environment.NewLine);

            if (!isAudioOnlyDownload) mergeVideoAndAudio();
            else addMetadata();
        }

        public void mergeVideoAndAudio() {
            // Merges .mp4 file and .mp3 file

            if (!File.Exists($"./temp/{fileNameLatestCreationTime}.mp3") || !File.Exists($"./temp/{fileNameLatestCreationTime}.mp4"))
            {
                consoleOutputTextbox.Text += "ERROR: Unable to merge -> video/audio file not found!";
                restart();
                return;
            }

            consoleOutputTextbox.AppendText(Environment.NewLine);
            consoleOutputTextbox.Text += "Merging video and audio...";
            consoleOutputTextbox.AppendText(Environment.NewLine);
            String mergeAudioVideo_arg = $"-i \"./temp/{fileNameLatestCreationTime}.mp4\" -i \"./temp/{fileNameLatestCreationTime}.mp3\" -c copy -map 0 -map 1:a:0 \"./temp/{fileNameLatestCreationTime}-temp.mp4\" -y";
            Process mergeAudioVideo = new Process();
            mergeAudioVideo.StartInfo.FileName = @".\ffmpeg";
            mergeAudioVideo.StartInfo.Arguments = mergeAudioVideo_arg;
            if (!showProgress) mergeAudioVideo.StartInfo.CreateNoWindow = true;
            mergeAudioVideo.EnableRaisingEvents = true;
            mergeAudioVideo.Exited += (s, args) =>
            {
                consoleOutputTextbox.Text += "Merging complete!";
                consoleOutputTextbox.AppendText(Environment.NewLine);

                addMetadata();
            };
            mergeAudioVideo.Start();   
        }

            public void addMetadata() {
            // Merges metadata with .mp3/.mp4 file depending on download type

            consoleOutputTextbox.AppendText(Environment.NewLine);
            if (!isAudioOnlyDownload) consoleOutputTextbox.Text += "Attaching metadata to video...";
            else consoleOutputTextbox.Text += "Attaching metadata to audio...";
            consoleOutputTextbox.AppendText(Environment.NewLine);
            String addMetadata_arg;
            if (!noThumbnail && !noThumbnailDownload) addMetadata_arg = $"-i \"./temp/{fileNameLatestCreationTime}.mp3\" -i \"./temp/{fileNameLatestCreationTime}.jpg\" -map 0:0 -map 1:0 -codec copy -id3v2_version 3 -metadata:s:v title=\"Album cover\" -metadata:s:v comment=\"Cover (front)\" \"./temp/{fileNameLatestCreationTime}-temp.mp3\" -y";
            else addMetadata_arg = $"-i \"./temp/{fileNameLatestCreationTime}.mp3\" -c copy \"./temp/{fileNameLatestCreationTime}-temp.mp3\" -y";
            if (!isAudioOnlyDownload)
            {
                var descArray = File.ReadAllText($"./temp/{fileNameLatestCreationTime}.description");
                String desc = String.Join('\n', descArray);
                desc = desc.Replace("\"", "''");

                if (ytURL.Contains("youtube.com"))
                {
                    String ytURLId = ytURL.Substring(ytURL.IndexOf("=") + 1, 11);
                    desc += $"\n\nhttps://youtu.be/{ytURLId}";
                } 
                else if (ytURL.Contains("youtu.be"))
                {
                    String ytURLTemp = (ytURL.Contains("https://")) ? ytURL.Substring(8) : ytURL;
                    desc += $"\n\nhttps://youtu.be/{ytURLTemp.Substring(ytURLTemp.IndexOf("/") + 1, 11)}";
                }

                if (!noThumbnail && !noThumbnailDownload) addMetadata_arg = $"-i \"./temp/{fileNameLatestCreationTime}-temp.mp4\" -i \"./temp/{fileNameLatestCreationTime}.jpg\" -map 1 -map 0 -c copy -disposition:0 attached_pic -metadata comment=\"{desc}\" -metadata author=\"{uploaderName}\" -metadata creation_time=\"{uploadDate}\" \"./temp/{fileNameLatestCreationTime}.mp4\" -y";
                else addMetadata_arg = $"-i \"./temp/{fileNameLatestCreationTime}-temp.mp4\" -c copy -metadata comment=\"{desc}\" -metadata author=\"{uploaderName}\" -metadata creation_time=\"{uploadDate}\" \"./temp/{fileNameLatestCreationTime}.mp4\" -y";
            }
            Process addMetadata = new Process();
            addMetadata.StartInfo.FileName = @".\ffmpeg";
            addMetadata.StartInfo.Arguments = addMetadata_arg;
            if (!showProgress) addMetadata.StartInfo.CreateNoWindow = true;
            addMetadata.EnableRaisingEvents = true;
            addMetadata.Exited += (s, args) =>
            {
                consoleOutputTextbox.Text += "Metadata attachment complete!";
                consoleOutputTextbox.AppendText(Environment.NewLine);

                finish();
            };
            addMetadata.Start();
        }

        public void finish() {
            // Moves files to download dir, writes download log

            consoleOutputTextbox.AppendText(Environment.NewLine);
            consoleOutputTextbox.Text += "Moving files...";
            consoleOutputTextbox.AppendText(Environment.NewLine);
            Directory.CreateDirectory(finalFileDir);
            if (!isAudioOnlyDownload)
            {
                try { File.Move($"./temp/{fileNameLatestCreationTime}.mp4", $"{finalFileDir}{fileNameLatestCreationTime}.mp4", true); }
                catch
                {
                    restoreDownloadDir(true);
                    File.Move($"./temp/{fileNameLatestCreationTime}.mp4", $"{finalFileDir}{fileNameLatestCreationTime}.mp4", true);
                }
            }
            else
            {
                try { File.Move($"./temp/{fileNameLatestCreationTime}-temp.mp3", $"{finalFileDir}{fileNameLatestCreationTime}.mp3", true); }
                catch
                {
                    restoreDownloadDir(true);
                    File.Move($"./temp/{fileNameLatestCreationTime}-temp.mp3", $"{finalFileDir}{fileNameLatestCreationTime}.mp3", true);
                }
            }
            /*if (File.Exists($"./temp/{fileNameLatestCreationTime}.en.vtt"))
            {
                File.Move($"./temp/{fileNameLatestCreationTime}.en.vtt", $"{finalFileDir}{fileNameLatestCreationTime}.en.vtt", true);
            }*/
            consoleOutputTextbox.Text += "File move complete!";
            consoleOutputTextbox.AppendText(Environment.NewLine);

            consoleOutputTextbox.AppendText(Environment.NewLine);
            consoleOutputTextbox.Text += "Download complete!";

            writeLog(fileNameLatestCreationTime);
            if (File.Exists($"{finalFileDir}{fileNameLatestCreationTime}.mp4"))
            {
                lastVideoDownloadedPath = $"{finalFileDir}{fileNameLatestCreationTime}.mp4";
                playVideoButton.Enabled = true;
            }
            else if (File.Exists($"{finalFileDir}{fileNameLatestCreationTime}.mp3"))
            {
                lastVideoDownloadedPath = $"{finalFileDir}{fileNameLatestCreationTime}.mp3";
                playVideoButton.Enabled = true;
            }

            consoleOutputTextbox.SelectionStart = consoleOutputTextbox.Text.Length;
            consoleOutputTextbox.ScrollToCaret();

            tryCount = 0;
            restart();
        }

        public void restoreDownloadDir (bool isError)
        {
            File.WriteAllText(downloadPathFile, Path.GetFullPath("./Downloads/"));
            finalFileDir = Path.GetFullPath("./Downloads/");

            if (isError)
            {
                consoleOutputTextbox.AppendText(Environment.NewLine);
                consoleOutputTextbox.Text += "Unable to move file(s) to downloads folder!";
                consoleOutputTextbox.AppendText(Environment.NewLine);
                consoleOutputTextbox.Text += $"File has been moved to \"{finalFileDir}\"";
            }
        }

        public void writeLog(String fileNameLatestCreationTime)
        {
            DateTime today = DateTime.Now;
            String logFilePath = "./download-history.txt";
            
            String logMsg = $"{today.Year}-{today.Month}-{today.Day} {today.Hour}:{today.Minute}  -  {fileNameLatestCreationTime}  -  {ytURL}";
            if (!isAudioOnlyDownload) logMsg += $"  -  Video";
            else logMsg += $"  -  Audio";
            if (noThumbnailDownload) logMsg += $"; No thumbnail";
            if (subDownloaded) logMsg += $"; Subtitles";

            if (writeLogFile && File.Exists(logFilePath)) File.AppendAllText((logFilePath), "\n" + logMsg);
            else if (writeLogFile) File.AppendAllText((logFilePath), logMsg);
        }

        public void writeToConfigFile()
        {
            String newConfig = JsonSerializer.Serialize(configValues);
            File.WriteAllText(configFile, newConfig);
        }

        public void writeDefaultConfigFile()
        {
            String defaultConfig = JsonSerializer.Serialize(new Dictionary<String, bool>
                {
                    { "autoBestQuality", false },
                    { "downloadSubtitles", true },
                    { "writeLogFile", true },
                    { "debugMode", false },
                    { "showProgress", false },
                    { "noThumbnailDownload", false }
                });
            File.WriteAllText(configFile, defaultConfig);
        }

        public void setAllButtonsEnabled(bool value)
        {
            qualitySelectListBox.Enabled = value;
            startDownloadButton.Enabled = value;
            changeDownloadsFolderToolStripMenuItem.Enabled = value;
            writeLogsToolStripMenuItem.Enabled = value;
            showProgressToolStripMenuItem.Enabled = value;
            videoRadioButton.Enabled = value;
            audioRadioButton.Enabled = value;
            subtitlesToolStripMenuItem.Enabled = value;
            newToolStripMenuItem.Enabled = value;
            autoBestQualityToolStripMenuItem.Enabled = value;
            resetDefaultConfigToolStripMenuItem.Enabled = value;
            noThumbnailToolStripMenuItem.Enabled = value;
            checkForYtdlpUpdateToolStripMenuItem.Enabled = value;
            restoreDefaultDownloadsFolderToolStripMenuItem.Enabled = value;
        }

        public void restart() {
            // Resets elements to initial states

            selectedQuality = 0;
            ytURL = "";
            try { availableQ.Clear(); }
            catch { MessageBox.Show("Error1"); }
            urlTextBox.Text = "";
            try { qualitySelectListBox.Items.Clear(); }
            catch { MessageBox.Show("Error2"); }
            setAllButtonsEnabled(true);
            disableEnter = false;
            subDownloaded = false;
            isAudioOnlyDownload = false;
            doNotStartDownload = false;


            if (!debugMode) {
                try {
                    List<String> tempFiles = new List<String>(Directory.EnumerateFiles("./temp/"));
                    foreach (String tempFile in tempFiles)
                    {
                        try { File.Delete(tempFile); }
                        catch { continue; }
                    }
                }
                catch { return; }
            }
        }   

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { thumbnailPreviewImage.Dispose(); }
            catch (Exception ex) { Console.Out.Write(ex.ToString); }
            try {
                List<String> tempFiles = new List<String>(Directory.EnumerateFiles("./temp/"));
                foreach (String tempFile in tempFiles)
                {
                    try { File.Delete(tempFile); }
                    catch { continue; }
                }
            }
            catch { return; }
        }
    }
}