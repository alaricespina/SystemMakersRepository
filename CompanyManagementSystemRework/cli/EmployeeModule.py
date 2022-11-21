import pandas as pd

class EmployeeModule:
    def __init__(self):
        self.file_name = "Datas/Employee_Dataset.csv"
        self.load_data()

    def createEmployee(self):
        # Add Employee
        pass 

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
    def showEmployee(self, id = 0, singular = False):
        # Just calls the print dataframe function
        FULL_LENGTH = False
        if FULL_LENGTH:
            len_print = len(self.employee_data[self.employee_data.columns.tolist()[0]])
        else:
            len_print = 100

        self.print_dataframe(self.employee_data, len_print, 3)

    # UTILITY FUNCTIONS 
    def print_dataframe(self, df, UPPER_LIMIT = 100, PADDING = 3):
        '''
        Printing the given dataframe, can be any of the following:
        - Actual dataframe of the csv file
        - Cut dataframe (defined by UPPER LIMIT VARIABLE)
        - Modified dataframe from search command
        '''

        # Find the lengths to be used in right-justify
        maxes = self.find_length_per_column(df)
        
        # Print a blank element to space the columns accordingly
        print("".ljust(len(str(UPPER_LIMIT))), end = "")
        for index, element in enumerate(df.columns.tolist()):
            print(element.rjust(maxes[index] + PADDING), end = "  ")
        print()

        # Printing of each row
        for row in range(0, UPPER_LIMIT):

            # If printing the search dataframe, there would be blank indexes
            # This would be for skipping those
            if row not in df.index:
                continue 

            print(str(row).ljust(len(str(UPPER_LIMIT))), end = "")
            for index, element in enumerate(df.columns.tolist()):
                length_spacing = maxes[index] + PADDING
                print(str(df[element][row]).rjust(length_spacing),end ="  ")

            print()

    def find_length_per_column(self, df):
        '''
        Finds the length of the texts in each column which will be used as length for the right-justify (rjust)
        Purpose: 
        Dataframe printing by line since pandas does not have a print by line without showing
        the column above it
        '''
        columns = df.columns.tolist()
        lengths = []

        for col in columns:
            a = [len(str(i)) for i in df[col].tolist()]
            lengths.append(max(a))

        return lengths

    def save_data(self):
        self.employee_data.to_csv(self.file_name)
    
    def load_data(self):
        self.employee_data = pd.read_csv(self.file_name)
        try:
            self.employee_data.drop("#", axis=1, inplace=True)
        except:
            pass
        
         

if __name__ == "__main__":
    EM = EmployeeModule()
    EM.searchEmployee()