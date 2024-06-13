using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Windows.System;

namespace text_test;

public partial class MainPage : ContentPage
{
    string path;

    public MainPage()
    {
        InitializeComponent();
    }


    private async void OpenBtn_Clicked(object sender, EventArgs e)
    {


        FileResult fileResult = await OpenFileDialog();
        if (fileResult != null)
        {
            path = fileResult.FullPath;
            OpenedFile.Text = $"File: {fileResult.FileName}";
            Read();

        }

    }



    private void Read()
    {
        if (File.Exists(path))
        {
            using (StreamReader sr = new StreamReader(path))
            {
                EOut.Text = sr.ReadToEnd();
            }
        }
    }

    private async Task<FileResult> OpenFileDialog()
    {
        FilePickerFileType fileType = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<String>>
            {
                {DevicePlatform.WinUI, new[]{".txt", ".csv", ".bat"} }
            });

        PickOptions pickOptions = new PickOptions()
        {
            PickerTitle = "Vyber textový soubor",
            FileTypes = fileType
        };

        return await FilePicker.Default.PickAsync(pickOptions);


    }

    private async void SaveBtn_Clicked(object sender, EventArgs e)
    {
        string a;

        using (StreamReader sr = new StreamReader(path))
        {
            a = sr.ReadToEnd().ToLower().Replace(";", "\n");
        }

        string path2 = Path.GetDirectoryName(path);
        string path3 = Path.Combine("G:", "Uprava.txt");
        

        using (StreamWriter sw = new StreamWriter(path3, true))
        {
            sw.WriteLine(a.ToString());
        }
        using (StreamReader sr = new StreamReader(path3))
        {
            EOut.Text = sr.ReadToEnd();
        }
    }
}
