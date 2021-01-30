# ResampleElectrons_Simple
Resampling electrons to 1mm resolution for IROC submission

# Important Rules
There are a couple of important rules in to keep in mind when using this tool.
1. The energy of the file is coming from the file name.
2. The Applicator size is hard-coded to 15 (since that's what we use for output). Please feel free to change this after the re-sampled files are generated.
3. IROC requests the machine model and Serial number be added to the file, so please add that after files are generated.

# Preparing Your Data
Open Beam Configuration. Select your machine, (1) Energy, (2) Applicator, and (3) Measurement to export.
![Select Data](https://github.com/WUSTL-ClinicalDev/ResampleElectrons_Simple/blob/master/PDD_Resample/DescriptionImages/1_OpenCurve.PNG)

Once the scan is selected, go to **File** --> **Export** --> **Curve**

![Export Data](https://github.com/WUSTL-ClinicalDev/ResampleElectrons_Simple/blob/master/PDD_Resample/DescriptionImages/2_exportCurve.PNG)

Save the file. 
Note: It is recommended to save the file with the energy name as it will be used to set the Energy in the file. For example, this file name will be **9E**

**Repeat for all energies to be re-sampled**

# Resample Your Data
Run the application. You may either do this from Visual Studio or compile the .exe and run that. If you don't have Visual Studio (or don't know how to use VS) please request a compiled exe from matthew.schmidt@wustl.edu

![Run Application](https://github.com/WUSTL-ClinicalDev/ResampleElectrons_Simple/blob/master/PDD_Resample/DescriptionImages/3_Start_Application.PNG)

Once the application runs, a File Dialog should pop up asking you to select a file. Please select one file in the folder that contains all of your scans (and only the exported curve data for scans). This application will read every file in the directory and try to re-sample it so please put them in a directory by themselves.

![Select File](https://github.com/WUSTL-ClinicalDev/ResampleElectrons_Simple/blob/master/PDD_Resample/DescriptionImages/4_File_Dialog_Import.PNG)

Once the tool has run, re-sampled files should be in the directory along with your exported files. 
![Output](https://github.com/WUSTL-ClinicalDev/ResampleElectrons_Simple/blob/master/PDD_Resample/DescriptionImages/5_output.PNG)

In the output files, please add your Machine Model and Serial number for swift identification by the IROC team.
![FileText](https://github.com/WUSTL-ClinicalDev/ResampleElectrons_Simple/blob/master/PDD_Resample/DescriptionImages/6_OutputOpen.PNG)

Reminder, you may need to change the applicator size if you're not using A15 for reference. You will also want to include your **machine model** and **serial number** in the files.
