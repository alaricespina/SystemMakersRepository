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

    # DONE NC
    def searchEmployee(self):
        search_df = self.employee_data
        while True:

            print("Available Search Options:")
            availables = search_df.columns.tolist()
            for index, a in enumerate(availables):
                print(f"{index} {a}")

            print()

            # Column
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
            
            # Element in Column

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
        self.print_dataframe(self.employee_data, 100, 3)

    # UTILITY FUNCTIONS 
    def print_dataframe(self, df, UPPER_LIMIT = 100, PADDING = 3):
        maxes = self.find_length_per_column(df)
        # Titles

        print("Length:")
        print(len(df[df.columns.tolist()[0]]))

        print("".ljust(len(str(UPPER_LIMIT))), end = "")
        for index, element in enumerate(df.columns.tolist()):
            print(element.rjust(maxes[index] + PADDING), end = "  ")

        print()

        # Each Row

        for row in range(0, UPPER_LIMIT):
            if row not in df.index:
                continue 

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