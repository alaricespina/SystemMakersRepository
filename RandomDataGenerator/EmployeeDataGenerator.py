import random 
import pandas as pd 
import os 



class DataGenerator():
    def __init__(self):
        self.output_file_name = "RandomEmployeeData.csv"
        self.female_names_file = "FemaleNames.csv"
        self.male_names_file = "MaleNames.csv"
        self.positions_file = "Positions.csv"
        self.surnames_file = "Surnames.csv"

    def load_data(self):
        self.female_names = pd.read_csv(self.female_names_file)
        self.male_names = pd.read_csv(self.male_names_file)
        self.positions = pd.read_csv(self.positions_file)
        self.surnames = pd.read_csv(self.surnames_file)     

    def query(self):
        while True:
            raw = input("How many rows must be generated?")  
            try:
                int(raw)
                break
            except:
                print("Invalid Input")

        return int(raw)

    def generateRow(self, amount):
        self.load_data()
        #print(self.female_names)
        for x in self.female_names.iloc[1]:
            print(x)
        #print(self.male_names)
        #print(self.positions)
        #print(self.surnames)

        
        pass


if __name__ == "__main__":
    DG = DataGenerator()
    #amount = DG.query()
    amount = 0
    DG.generateRow(amount)
    

