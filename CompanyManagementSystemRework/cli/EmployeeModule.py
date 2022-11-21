import pandas as pd

class EmployeeModule:
    def __init__(self):
        self.employee_data = pd.read_csv("Datas/Employee_Dataset.csv")
        self.employee_data.drop("#", axis=1, inplace=True)

    def createEmployee(self):
        pass 

    def editEmployee(self):
        pass 

    def deleteEmployee(self):
        pass 

    def searchEmployee(self):
        availables = self.employee_data.columns.tolist()
        print("Available Search Options:")
        for a in availables:
            print(a, end = ", ")

        print()

        while True:
            search_criteria = input("->").lower()
            criterion = [x.lower() for x in availables]

            if search_criteria not in criterion:
                print("Search criteria not found")
            else:
                break

        print("Search criteria found!")

    def filterEmployee(self):
        pass 

    def showEmployee(self, id = 0, singular = False):
        self.print_dataframe(self.employee_data, 100, 3)
        

        

    def print_dataframe(self, df, UPPER_LIMIT = 100, PADDING = 3):
        maxes = self.find_length_per_column(df)
        # Titles

        print("".ljust(len(str(UPPER_LIMIT))), end = "")
        for index, element in enumerate(df.columns.tolist()):
            print(element.rjust(maxes[index] + PADDING), end = "  ")

        print()

        # Each Row

        for row in range(0, UPPER_LIMIT):
            print(str(row).ljust(len(str(UPPER_LIMIT))), end = "")
            for index, element in enumerate(df.columns.tolist()):
                length_spacing = maxes[index] + PADDING
                print(str(df[element][row]).rjust(length_spacing),end ="  ")

            print()

        


    def find_length_per_column(self, df):
        columns = df.columns.tolist()
        lengths = []

        for col in columns:
            a = [len(str(i)) for i in df[col].tolist()]
            lengths.append(max(a))

        return lengths

        
         

if __name__ == "__main__":
    EM = EmployeeModule()
    EM.searchEmployee()