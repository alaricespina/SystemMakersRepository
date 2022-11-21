import pandas as pd

df = pd.read_csv("Datas/Employee_Dataset.csv")
df.drop("#", axis=1, inplace=True)




if __name__ == "__main__":
    

