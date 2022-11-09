def inventory_text():
    return [
        "Inventory Management",
        "[1] Add Item",
        "[2] Edit Item",
        "[3] Remove Item",
        "[4] Group Items",
        "[5] View Items",
        "[6] Search Item",
    ]

def finance_text():
    return [
        "Finance Management",
        "[1] Add Transaction",
        "[2] Edit Transaction",
        "[3] Remove Transaction",
        "[4] View Transactions",
        "[5] Search Transaction"
    ]

def project_text():
    return [
        "Project Management",
        "[1] Add Project",
        "[2] Edit Project",
        "[3] Remove Project",
        "[4] View Projects",
        "[5] Search Project"
    ]

def employee_text():
    return [ 
        "Employee Management",
        "[1] Add Employee",
        "[2] Edit Employee Details",
        "[3] Remove Employee",
        "[4] View Employee",
        "[5] Search Employee"
    ]

def main_menu_text():
    return [
        "Welcome to Company Management System",
        "Options:",
        "[1] Inventory",
        "[2] Finance",
        "[3] Project",
        "[4] Employee"
    ]
    
def array_printer(array):
    for line in array:
        print(line)

def evaluate_choice(choice, choices):
    if choice in choices:
        exec(choices[choice])
    else:
        return print("Error")

def main():
    
    pass 

if __name__ == "__main__":
    main()