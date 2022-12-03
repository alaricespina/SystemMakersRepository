import pandas as pd
import UtilityModule

class EmployeeModule:
    def __init__(self):
        self.file_name = "Datas/Employee_Dataset.csv"
        self.employee_data = pd.read_csv(self.file_name)

    def createEmployee(self):
        '''
        Creates a new employee through user input and adds new employee data to the current list
        '''
        first_name = input("Enter First Name:") 
        last_name =  input("Enter Last Name")
        position =  input("Enter Position:")
        gender = input("Enter Gender:")
        salary = input("Enter Salary:")

        Employee_Data = {
            "First Name" : first_name,
            "Last Name" : last_name,
            "Positions" : position.upper(),
            "Gender" : gender.capitalize(),
            "Salary" : int(salary)
        }

        self.employee_data = self.employee_data.append(self.employee_data, ignore_index=True)

        if (input("Show new employee list? [Y for yes]:").upper() == "Y"):
            self.showEmployee(full_length=True)
        
        self.employee_data.to_csv(self.file_name)

        
        

    def editEmployee(self):
        # Search for Employee First
        # Ask for new details
        pass 

    def deleteEmployee(self):
        # Search for Employee First
        # Ask for new details
        pass 

    # DONE NC
    def searchEmployee(self):
        '''
        Searches the employee based on the given parameters by the user
        Repetitive, until the user wants to exit
        Logically Irreversible, cannot undo the current filter used
        Ex: Gender -> Salary -> Position. Cannot go back to Gender after position
        '''

        search_df = self.employee_data

        # Repetitive
        while True:
            
            # Prints available search options
            print("Available Search Options:")
            availables = search_df.columns.tolist()
            for index, a in enumerate(availables):
                print(f"{index} {a}")

            print()

            # Looks for the column, error checked
            while True:
                search_criteria = input("->").lower()
                criterion = [x.lower() for x in availables]

                if search_criteria.lower() not in criterion:
                    print("Search criteria not found")
                elif search_criteria.lower() == "none" or search_criteria.lower() == "back":
                    return search_df
                else:
                    break

            print("Search criteria found!")
            
            # Checks if element exists, unless numeric, error checked
            av_search_criterion = search_df[search_criteria.title()].unique().tolist()
            for index, element in enumerate(av_search_criterion):
                print(f"{index} {element}")

            while True:
                criteria_search = input("->")
                if type(av_search_criterion[0]) == str:
                    av = [x.lower() for x in av_search_criterion]
                
                    if criteria_search.lower() not in av:
                        print("Item not found")
                    elif criteria_search.lower() == "none" or criteria_search.lower() == "back":
                        return search_df
                    else:
                        break 
                else:
                    break
            
            # Different criteria for different columns
            # Positions have all values in uppercase
            # Salary have numeric values
            # Everything else is in Capitalize Each Word mode
            if search_criteria.lower() == "positions":
                a = search_df[search_df[search_criteria.title()] == criteria_search.upper()]
            elif search_criteria.lower() == "salary":
                a = search_df[search_df[search_criteria.title()] >= int(criteria_search)]
            else:
                a = search_df[search_df[search_criteria.title()] == criteria_search.title()]

            choice = input("Print Head? [Y for yes]")
            if choice.lower() == "y":
                print(a)

            search_df = a 

            choice = input("Exit? [Y for yes]")
            if choice.lower() == "y":
                break
        
        return search_df
    
    # DONE NC
    def showEmployee(self, singular = False, full_length = False):
        # Just calls the print dataframe function
        if full_length:
            len_print = len(self.employee_data[self.employee_data.columns.tolist()[0]])
        else:
            len_print = 100

        UtilityModule.print_dataframe(self.employee_data, len_print)

    
        
         

if __name__ == "__main__":
    EM = EmployeeModule()
    EM.createEmployee()
    #EM.showEmployee()