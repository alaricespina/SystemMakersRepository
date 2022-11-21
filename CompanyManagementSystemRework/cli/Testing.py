import pandas as pd

def main():
    # Load Data
    df = pd.read_csv("Datas/Employee_Dataset.csv")

    # Remove Index Reading (Artifact from saving to csv using notebook)
    df.drop("#", axis=1, inplace=True)

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

    df = df.append(Employee_Data, ignore_index=True)
    print(df.tail())
    #print_dataframe(df)
    

# UTILITY FUNCTIONS 
def print_dataframe(df, UPPER_LIMIT = 100, PADDING = 3):
    '''
    Printing the given dataframe, can be any of the following:
    - Actual dataframe of the csv file
    - Cut dataframe (defined by UPPER LIMIT VARIABLE)
    - Modified dataframe from search command
    '''

    # Find the lengths to be used in right-justify
    maxes = find_length_per_column(df)
    
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

def find_length_per_column(df):
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

  


# Safety Feature, only runs if this is the main file
if __name__ == "__main__":
    print("Running File")
    main()

