import pandas as pd

class EmployeeModule:
    def __init__(self):
        self.employee_data = pd.read_csv("Datas/Employee_Dataset.csv")
        self.employee_data.drop("#", axis=1, inplace=True)

        print(self.employee_data.columns)

    def createEmployee(self):
        pass 

    def editEmployee(self):
        pass 

    def deleteEmployee(self):
        pass 

    def searchEmployee(self):
        pass 

    def filterEmployee(self):
        pass 

    def showEmployee(self, id = 0, singular = False):
        print(self.employee_data.loc[["First Name", "Last Name"]])
        

        
        pass 

        
         

if __name__ == "__main__":
    EM = EmployeeModule()
    EM.showEmployee()
    print("Employee Module")