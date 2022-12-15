import pandas as pd
import UtilityModule

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
    UtilityModule.print_dataframe(df)
    

  


# Safety Feature, only runs if this is the main file
if __name__ == "__main__":
    print("Running File")
    main()

