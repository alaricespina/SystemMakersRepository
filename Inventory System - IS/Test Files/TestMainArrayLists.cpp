#include <iostream>
#include "ItemListType.h"
#include "ItemType.h"
#include <fstream>
#include <string>
#include <iomanip>


using namespace std;

Item testItem;

ifstream inFile;
string name;
string brand;
string category;
int quantity;
double cost;
string comments;
char comma_holder;

int main(){
    inFile.open("Matinong_Car_Data.csv");
    ItemListType hatdog(100);

    while (true){
        getline(inFile, category, ',');
        getline(inFile, brand, ',');
        getline(inFile, name, ',');
        inFile >> quantity;
        inFile >> comma_holder;
        inFile >> cost;
        inFile >> comma_holder;
        getline(inFile, comments, '\n');
        testItem.set_Details(name, brand, category, quantity, cost, comments);
        
        if (inFile.eof())
            break;
        hatdog.insertEnd(testItem);
    }

    hatdog.print();


    return 0;
}