import pandas as pd

df = pd.read_csv("Datas/Employee_Dataset.csv")
df.drop("#", axis=1, inplace=True)

def find_length_per_column(df):
    columns = df.columns.tolist()
    lengths = []

    for col in columns:
        a = [len(str(i)) for i in df[col].tolist()]
        lengths.append(max(a))

    return lengths

maxes = find_length_per_column(df)
for x in maxes:
    print(x)

# UPPER_LIMIT = len(df["First Name"])
UPPER_LIMIT = 100
PADDING = 3
# Titles

print("".ljust(len(str(UPPER_LIMIT))), end = "")
for index, element in enumerate(df.columns.tolist()):
    print(element.rjust(maxes[index] + PADDING), end = "  ")

print()

# Each Row

for row in range(0, UPPER_LIMIT):
    print(str(row).ljust(len(str(UPPER_LIMIT))), end = "")
    for index, element in enumerate(df.columns.tolist()):
        length_spacing = maxes[index] + PADDING
        print(str(df[element][row]).rjust(length_spacing),end ="  ")

    print()



