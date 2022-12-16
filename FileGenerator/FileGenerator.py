import random 
import string 
import os
import datetime

class FileGeneratorType:
    class TypeNumeric:
        def __init__(self) -> None:
            self.CONTENT = string.digits 
    
    class TypeString:
        def __init__(self) -> None:
            self.CONTENT = string.ascii_letters

    class TypeAlphaNumeric:
        def __init__(self) -> None:
            self.CONTENT = string.ascii_letters + string.digits
    
    class TypeAll:
        def __init__(self) -> None:
            self.CONTENT = ''.join(string.printable[:len(string.printable)-6])
    
    class TypeCustom:
        def __init__(self, custom_content) -> None:
            self.CONTENT = custom_content
    
    class TypeDisabled:
        def __init__(self) -> None:
            self.CONTENT = None
    
class FileGenerator:
    def __init__(self, debug_mode = False) -> None:
        self.debug_mode = debug_mode

    def generateContent(self, file_object, file_size, file_content_list):
        for _ in range(file_size):
            file_object.write(random.choice(file_content_list))

    def generateRandomFile(self, file_name, file_size, MODE = FileGeneratorType.TypeAll()):
        if self.debug_mode: print(f"{datetime.datetime.now().strftime('%H:%M:%S')} - Generating {file_name} with size {file_size}")
        with open(f"{file_name}", "w") as file_object:
            self.generateContent(file_object, file_size, MODE.CONTENT)
    
    def generateRandomFiles(self, file_name, file_size, file_num, random_file_name_length = 10, file_name_mode = FileGeneratorType.TypeAlphaNumeric(), file_content_mode = FileGeneratorType.TypeAll()):
        if self.debug_mode: print(f"{datetime.datetime.now().strftime('%H:%M:%S')} - Generating {file_num} files each with size {file_size}")
        print(file_name)
        for i in range(file_num):
            if type(file_name_mode) == type(FileGeneratorType.TypeDisabled()):
                output_file_name = f"{file_name.split('.')[0]} {i}.{file_name.split('.')[1]}"
                print(output_file_name)
                self.generateRandomFile(file_name=output_file_name, file_size=file_size, MODE=file_content_mode)
            else:
                random_string = ''.join(random.choices(file_name_mode.CONTENT, k=random_file_name_length))
                output_file_name = f"{file_name.split('.')[0]} {random_string}.{file_name.split('.')[1]}"
                print(output_file_name)
                self.generateRandomFile(file_name=output_file_name, file_size=file_size, MODE=file_content_mode)


    def generateRandomFolder(self, file_name, file_size, file_num, FOLDER_NAME, filename_lengths = 10, filename_format = FileGeneratorType.TypeAlphaNumeric(), filecontent_format = FileGeneratorType.TypeAll()):
        if self.debug_mode: print(f"{datetime.datetime.now().strftime('%H:%M:%S')} - Generating {FOLDER_NAME} with {file_num} Files")
                
        output_file_name = FOLDER_NAME + "/" + file_name
        if not(os.path.isdir(FOLDER_NAME)):
            os.mkdir(FOLDER_NAME)
        
        self.generateRandomFiles(file_name = output_file_name, file_size=file_size, file_num = file_num, random_file_name_length=filename_lengths, file_name_mode=filename_format, file_content_mode=filecontent_format)

    def generateRandomFolders(self, file_name, file_sizes, file_num, FOLDER_NAME, folder_num, filename_lengths = 10, foldername_lengths = 10,filename_format = FileGeneratorType.TypeAlphaNumeric(), foldername_format=FileGeneratorType.TypeAlphaNumeric(), filecontent_format = FileGeneratorType.TypeAll()):
        if self.debug_mode: print(f"{datetime.datetime.now().strftime('%H:%M:%S')} - Generating {folder_num} folders named {FOLDER_NAME} with {file_num} files each")

        for i in range(folder_num):
            if type(foldername_format) == type(FileGeneratorType.TypeDisabled()):
                output_folder_name = FOLDER_NAME + " " + str(i)
                self.generateRandomFolder(file_name = file_name, file_size=file_sizes, file_num=file_num, FOLDER_NAME=output_folder_name, filename_lengths=filename_lengths, filename_format=filename_format, filecontent_format=filecontent_format)
            else:
                output_folder_name = FOLDER_NAME + " " + ''.join(random.choices(foldername_format.CONTENT, k=foldername_lengths)) 
                self.generateRandomFolder(file_name = file_name, file_size=file_sizes, file_num=file_num, FOLDER_NAME=output_folder_name, filename_lengths=filename_lengths, filename_format=filename_format, filecontent_format=filecontent_format)

def main():
    FG = FileGenerator(debug_mode=True)
    FG.generateRandomFolders("Yes.pkl", 100, 2, "Output", 2, filename_format=FileGeneratorType.TypeNumeric(), foldername_format=FileGeneratorType.TypeString(), filecontent_format=FileGeneratorType.TypeNumeric())


if __name__ == "__main__":
    main()