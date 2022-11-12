import random 
import pandas as pd 
import os 



class DataGenerator():
    def __init__(self):
        output_file_name = "RandomEmployeeData.csv"
        female_names_file = "FemaleNames.csv"
        male_names_file = "MaleNames.csv"
        positions_file = "Positions.csv"
        surnames_file = "Surnames.csv"

    def load_data(self, fnf, mnf, pf, sf):
        self.female_names = pd.read_csv(fnf)
        self.male_names = pd.read_csv(mnf)
        self.positions = pd.read_csv(pf)
        self.surnames = pd.read_csv(sf)     

    def query():
        while True:
            raw = input("How many rows must be generated?")  
            try:
                int(raw)
                break
            except:
                print("Invalid Input")

        return int(raw)

    def generateRow():
        pass

