import customtkinter 

from FileGenerator import FileGenerator, FileGeneratorType
from PIL import Image

customtkinter.set_appearance_mode("dark")
customtkinter.set_default_color_theme("green")

app = customtkinter.CTk()
app.geometry("1200x600")
app.title("File Generator")

DEBUG_MODE = False
APP_GREEN = "#2D6836"
APP_BLACK = "#272727"
APP_WHITE = "#e3e3e3"
##########################################################################################################################
# TEMPORARY BUTTON FUNCTIONS
##########################################################################################################################

def GenerateFolder():
    if (DEBUG_MODE) : print("Generate Folder Button has been pressed")
    try:
        Folder_Name = folder_folder_name_entry.get()
        File_Name = folder_file_name_entry.get() + "." + folder_file_name_extension_entry.get()
        Folder_Name_Generation_Mode = folder_folder_name_generation_mode_options.get()
        File_Name_Generation_Mode = folder_file_name_generation_mode_options.get()
        Content_Generation_Mode = folder_file_content_generation_mode_options.get()

        File_Size = int(folder_file_size_entry.get())
        File_Number = int(folder_number_of_files_entry.get())
        Folder_Number = int(folder_number_of_folders_entry.get())

        # Ternary = true value if condition else false value
        # ["Numeric", "Alphabetic", "AlphaNumeric", "Disabled"]
        FileName_Generation_Type = FileGeneratorType.TypeNumeric() if (File_Name_Generation_Mode == "Numeric") else FileGeneratorType.TypeString() if (File_Name_Generation_Mode == "Alphabetic") else FileGeneratorType.TypeAlphaNumeric() if (File_Name_Generation_Mode == "AlphaNumeric") else FileGeneratorType.TypeDisabled() if (File_Name_Generation_Mode == "Disabled") else None 

        FolderName_Generation_Type = FileGeneratorType.TypeNumeric() if (Folder_Name_Generation_Mode == "Numeric") else FileGeneratorType.TypeString() if (Folder_Name_Generation_Mode == "Alphabetic") else FileGeneratorType.TypeAlphaNumeric() if (Folder_Name_Generation_Mode == "AlphaNumeric") else FileGeneratorType.TypeDisabled() if (Folder_Name_Generation_Mode == "Disabled") else None 

        # ["Numeric", "Alphabetic", "AlphaNumeric", "All"]
        FileContent_Generation_Type = FileGeneratorType.TypeNumeric() if (Content_Generation_Mode == "Numeric") else FileGeneratorType.TypeString() if (Content_Generation_Mode == "Alphabetic") else FileGeneratorType.TypeAlphaNumeric() if (Content_Generation_Mode == "AlphaNumeric") else FileGeneratorType.TypeAll() if (Content_Generation_Mode == "All") else None

        if (FileContent_Generation_Type == None):
            raise Exception("Invalid File Content Generation Type")
        if (FolderName_Generation_Type == None):
            raise Exception("Invalid Folder Name Generation Type")
        if (FileName_Generation_Type == None):
            raise Exception("Invalid File Name Generation Type")

        if len(file_file_name_entry.get()) == 0 or len(file_file_name_extension_entry.get()) == 0:
            raise Exception("Invalid File Name")
        
        if len(folder_folder_name_entry.get()) == 0:
            raise Exception("Invalid Folder Name")


        debug_string = f"Generating {Folder_Number} Folders each with {File_Number} Files\nDetails per Folder\nBase Folder Name: {Folder_Name}\nBase File Name: {File_Name}\nSize: {File_Size} B\nFolder Name Generation Mode:{Folder_Name_Generation_Mode}\nFile Name Generation Mode: {File_Name_Generation_Mode}\nFile Content Generation Mode: {Content_Generation_Mode}"
        if (DEBUG_MODE) : print(debug_string)

        console_textbox.insert("end", "\n\n"+debug_string)

        if Folder_Number == 1:
            FileGenerator().generateRandomFolder(File_Name, File_Size, File_Number, filename_format=FileName_Generation_Type, filecontent_format=FileContent_Generation_Type)
        elif Folder_Number > 1:
            FileGenerator().generateRandomFolders(File_Name, File_Size, File_Number, Folder_Name, Folder_Number,filename_format=FileName_Generation_Type, filecontent_format=FileContent_Generation_Type, foldername_format=FolderName_Generation_Type)
        

    except ValueError as E:
        console_textbox.insert("end", "\n\n"+"Invalid File Size or Number of Files / Invalid Folder Name")

    except Exception as E:
        console_textbox.insert("end", "\n\n"+str(E))

    console_textbox.see("end")


def GenerateFile():
    if (DEBUG_MODE) : print("Generate File Button has been pressed")
    try:
        File_Name = file_file_name_entry.get() + "." + file_file_name_extension_entry.get()
        Name_Generation_Mode = file_file_name_generation_mode_options.get()
        Content_Generation_Mode = file_file_content_generation_mode_options.get()

        File_Size = int(file_file_size_entry.get())
        File_Number = int(file_number_of_files_entry.get())

        # Ternary = true value if condition else false value
        # ["Numeric", "Alphabetic", "AlphaNumeric", "Disabled"]
        FileName_Generation_Type = FileGeneratorType.TypeNumeric() if (Name_Generation_Mode == "Numeric") else FileGeneratorType.TypeString() if (Name_Generation_Mode == "Alphabetic") else FileGeneratorType.TypeAlphaNumeric() if (Name_Generation_Mode == "AlphaNumeric") else FileGeneratorType.TypeDisabled() if (Name_Generation_Mode == "Disabled") else None 

        # ["Numeric", "Alphabetic", "AlphaNumeric", "All"]
        FileContent_Generation_Type = FileGeneratorType.TypeNumeric() if (Content_Generation_Mode == "Numeric") else FileGeneratorType.TypeString() if (Content_Generation_Mode == "Alphabetic") else FileGeneratorType.TypeAlphaNumeric() if (Content_Generation_Mode == "AlphaNumeric") else FileGeneratorType.TypeAll() if (Content_Generation_Mode == "All") else None

        if (FileContent_Generation_Type == None):
            raise Exception("Invalid File Content Generation Type")
        if (FileName_Generation_Type == None):
            raise Exception("Invalid File Name Generation Type")

        if len(file_file_name_entry.get()) == 0 or len(file_file_name_extension_entry.get()) == 0:
            raise Exception("Invalid File Name")

        if (DEBUG_MODE) : print(FileName_Generation_Type.CONTENT)
        if (DEBUG_MODE) : print(FileContent_Generation_Type.CONTENT)

        debug_string = f"Generating {File_Number} Files\nDetails per File:\nBase File Name: {File_Name}\nSize: {File_Size} B\nFile Name Generation Mode: {Name_Generation_Mode}\nContent_Generation_Mode: {Content_Generation_Mode}"
        if (DEBUG_MODE) : print(debug_string)

        console_textbox.insert("end", "\n\n"+debug_string)

        if File_Number == 1:
            FileGenerator().generateRandomFile(File_Name, File_Size, FileContent_Generation_Type)
        elif File_Number > 1:
            FileGenerator().generateRandomFiles(File_Name, File_Size, File_Number, file_name_mode=FileName_Generation_Type, file_content_mode=FileContent_Generation_Type)


    except ValueError as E:
        console_textbox.insert("end", "\n\n"+"Invalid File Size or Number of Files")
    except Exception as E:
        console_textbox.insert("end", "\n\n"+str(E))
    

    console_textbox.see("end")
    

##########################################################################################################################

##########################################################################################################################
# HEADER PART
##########################################################################################################################

# Header Frame
header_frame = customtkinter.CTkFrame(master=app, fg_color=APP_GREEN, corner_radius=0, width=900, height=50)
header_frame.place(relwidth=1.0, relheight=0.1)

# App Title
app_title_frame = customtkinter.CTkFrame(master=header_frame, corner_radius=0, fg_color=APP_GREEN, width=300, height=50)
app_title_frame.place(relx=0, rely=0, relwidth=0.4, relheight=1.0)

app_title_font = customtkinter.CTkFont(family="Bahnschrift", size=22, weight="bold")
lbl_1 = customtkinter.CTkLabel(master=app_title_frame, text="FILE GENERATOR", font=app_title_font,corner_radius=0) # File Generator Title
lbl_1.place(relx=0, rely=0, relwidth=1.0, relheight=1.0)

# File Group header
file_frame = customtkinter.CTkFrame(master=header_frame, corner_radius=0, fg_color=APP_GREEN,width=300, height=50)
file_frame.place(relx=0.4, rely=0, relwidth=0.3, relheight=1.0)
File_Image = customtkinter.CTkImage(light_image = Image.open("Assets/FileIcon.png"), dark_image = Image.open("Assets/FileIcon.png"))
lbl_2_0 = customtkinter.CTkLabel(master=file_frame, image=File_Image, text="",  anchor="e") # Files Icon
lbl_2_1 = customtkinter.CTkLabel(master=file_frame, text="FILES",  anchor="w") # Files Title
lbl_2_0.place(relx=0.375, rely=0, relwidth=0.1, relheight=1.0)
lbl_2_1.place(relx=0.5, rely=0, relwidth=0.3, relheight=1.0)

# Folder Group header
folder_frame = customtkinter.CTkFrame(master=header_frame, corner_radius=0, fg_color=APP_GREEN,width=300)
folder_frame.place(relx=0.7, rely=0, relwidth=0.3, relheight=1.0)
Folder_Image = customtkinter.CTkImage(light_image = Image.open("Assets/FolderIcon.png"), dark_image = Image.open("Assets/FolderIcon.png"))
lbl_3_0 = customtkinter.CTkLabel(master=folder_frame, image=Folder_Image, text="", anchor="e") # Folder Icon
lbl_3_1 = customtkinter.CTkLabel(master=folder_frame, text="FOLDERS", anchor="w") # Folders Title
lbl_3_0.place(relx=0.375, rely=0, relwidth=0.1, relheight=1.0)
lbl_3_1.place(relx=0.5, rely=0, relwidth=0.3, relheight=1.0)

##########################################################################################################################

##########################################################################################################################
# BODY PART
##########################################################################################################################

# Body Grid Frame
body_frame = customtkinter.CTkFrame(master=app, fg_color=APP_BLACK, corner_radius=0)
body_frame.place(relwidth=1.0, relheight=0.9, relx=0, rely=0.1)

# Console Frame
console_frame = customtkinter.CTkFrame(master=body_frame, fg_color=APP_BLACK, corner_radius=0)
console_frame.place(relwidth=0.4, relheight=1, relx=0, rely=0)

console_textbox = customtkinter.CTkTextbox(master=console_frame)
console_textbox.insert("0.0", "This is at the beginning")
console_textbox.insert("end", "\nThis is at the end")
console_textbox.place(relheight=0.9, relwidth=0.9, relx=0.05, rely=0.05)

##########################################################################################################################

# File Group Frame
file_group_frame = customtkinter.CTkFrame(master=body_frame, fg_color=APP_BLACK, corner_radius=0)
file_group_frame.place(relwidth=0.3, relheight=1, relx=0.4, rely=0)

# Basic Options such as :
# file name, content generation mode, and 
# file name generation mode if randomized
basic_files_options_frame = customtkinter.CTkFrame(master=file_group_frame, fg_color=APP_BLACK, corner_radius=0)
basic_files_options_frame.place(relwidth=0.9, relheight= 0.4, relx = 0.05, rely=0.05)

file_file_name_entry = customtkinter.CTkEntry(master=basic_files_options_frame, placeholder_text="File Name", placeholder_text_color=APP_WHITE)
file_file_name_entry.place(relwidth=0.75, relheight=0.2, relx=0, rely=0)

file_file_name_extension_entry = customtkinter.CTkEntry(master=basic_files_options_frame, placeholder_text="Ext", placeholder_text_color=APP_WHITE)
file_file_name_extension_entry.place(relwidth=0.2, relheight=0.2, relx=0.8, rely=0)

file_file_content_generation_mode_options = customtkinter.CTkComboBox(master=basic_files_options_frame, values=["Numeric", "Alphabetic", "AlphaNumeric", "All"], text_color=APP_WHITE)
file_file_content_generation_mode_options.place(relwidth=1, relheight=0.2, relx=0, rely=0.25)
file_file_content_generation_mode_options.set("FILE CONTENT GENERATION MODE")

file_file_name_generation_mode_options = customtkinter.CTkComboBox(master=basic_files_options_frame, values=["Numeric", "Alphabetic", "AlphaNumeric", "Disabled"], text_color=APP_WHITE)
file_file_name_generation_mode_options.place(relwidth=1, relheight=0.2, relx=0, rely=0.5)
file_file_name_generation_mode_options.set("FILE NAME GENERATION MODE")

# Specific Options such as number of files, and size per file (in bytes)
specific_files_options_frame = customtkinter.CTkFrame(master=file_group_frame, fg_color=APP_BLACK, corner_radius=0)
specific_files_options_frame.place(relwidth=0.9, relheight=0.25, relx=0.05, rely=0.55)

file_number_of_files_entry = customtkinter.CTkEntry(master=specific_files_options_frame, placeholder_text="Number of Files", placeholder_text_color=APP_WHITE)
file_number_of_files_entry.place(relwidth=1, relheight=0.4, relx=0, rely=0)

file_file_size_entry = customtkinter.CTkEntry(master=specific_files_options_frame, placeholder_text="File Size (in Bytes)", placeholder_text_color=APP_WHITE)
file_file_size_entry.place(relwidth=1, relheight=0.4, relx=0, rely=0.5)

# Generate Button
generate_files_button = customtkinter.CTkButton(master=file_group_frame, text="GENERATE", command=GenerateFile, corner_radius=0)
generate_files_button.place(relwidth=0.9, relheight=0.075, relx=0.05, rely=0.875)

##########################################################################################################################

# Folder Group Frame
folder_group_frame = customtkinter.CTkFrame(master=body_frame, fg_color=APP_BLACK, corner_radius=0)
folder_group_frame.place(relwidth=0.3, relheight=1, relx=0.7, rely=0)

# Basic Options such as :
# Folder Name and File Name
# File Content Generation Mode, File and Folder name creation mode 
# both are conditional if they are enabled by the user
basic_folder_options_frame = customtkinter.CTkFrame(master=folder_group_frame, fg_color=APP_BLACK, corner_radius=0)
basic_folder_options_frame.place(relwidth=0.9, relheight = 0.475, relx=0.05, rely=0.05)

folder_folder_name_entry = customtkinter.CTkEntry(master=basic_folder_options_frame, placeholder_text="Folder Name", placeholder_text_color=APP_WHITE)
folder_folder_name_entry.place(relwidth=1, relheight=0.19, relx=0, rely=0)

folder_file_name_entry = customtkinter.CTkEntry(master=basic_folder_options_frame, placeholder_text="File Name", placeholder_text_color=APP_WHITE)
folder_file_name_entry.place(relwidth=0.75, relheight=0.19, relx=0, rely=0.2)

folder_file_name_extension_entry = customtkinter.CTkEntry(master=basic_folder_options_frame, placeholder_text="Ext", placeholder_text_color=APP_WHITE)
folder_file_name_extension_entry.place(relwidth=0.2, relheight=0.19, relx=0.8, rely=0.2)

folder_file_content_generation_mode_options = customtkinter.CTkComboBox(master=basic_folder_options_frame, values=["Numeric", "Alphabetic", "AlphaNumeric", "All"], text_color=APP_WHITE)
folder_file_content_generation_mode_options.place(relwidth=1, relheight=0.19, relx=0, rely=0.4)
folder_file_content_generation_mode_options.set("File Content Generation Mode")

folder_file_name_generation_mode_options = customtkinter.CTkComboBox(master=basic_folder_options_frame, values=["Numeric", "Alphabetic", "AlphaNumeric", "Disabled"], text_color=APP_WHITE)
folder_file_name_generation_mode_options.place(relwidth=1, relheight=0.19, relx=0, rely=0.6)
folder_file_name_generation_mode_options.set("File Name Generation Mode")

folder_folder_name_generation_mode_options = customtkinter.CTkComboBox(master=basic_folder_options_frame, values=["Numeric", "Alphabetic", "AlphaNumeric", "Disabled"], text_color=APP_WHITE)
folder_folder_name_generation_mode_options.place(relwidth=1, relheight=0.19, relx=0, rely=0.8)
folder_folder_name_generation_mode_options.set("Folder Name Generation Mode")

# Specific Options
specific_folder_options_frame = customtkinter.CTkFrame(master=folder_group_frame, fg_color=APP_BLACK, corner_radius=0)
specific_folder_options_frame.place(relwidth=0.9, relheight=0.3, relx=0.05, rely=0.55)

folder_number_of_files_entry = customtkinter.CTkEntry(master=specific_folder_options_frame, placeholder_text="Number of Files", placeholder_text_color=APP_WHITE)
folder_number_of_files_entry.place(relwidth=1, relheight=0.3, relx=0, rely=0)

folder_number_of_folders_entry = customtkinter.CTkEntry(master=specific_folder_options_frame, placeholder_text="Number of Folders", placeholder_text_color=APP_WHITE)
folder_number_of_folders_entry.place(relwidth=1, relheight=0.3, relx=0, rely=0.35)

folder_file_size_entry = customtkinter.CTkEntry(master=specific_folder_options_frame, placeholder_text="File Size (in Bytes)", placeholder_text_color=APP_WHITE)
folder_file_size_entry.place(relwidth=1, relheight=0.3, relx=0, rely=0.7)

# Generate Button
generate_folder_button = customtkinter.CTkButton(master=folder_group_frame, text="GENERATE", command=GenerateFolder, corner_radius=0)
generate_folder_button.place(relwidth=0.9, relheight=0.075, relx=0.05, rely=0.875)

app.mainloop()