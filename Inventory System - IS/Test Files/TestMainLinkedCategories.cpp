#include <iostream>
#include "ItemListType.h"
#include "ItemType.h"
#include "linkedCategories.h"
#include <fstream>
#include <string>
#include <iomanip>


using namespace std;

Item testItem;

ifstream inFile;
ifstream inFileCategories;

string name;
string brand;
string category;
string prev_category;
int quantity;
double cost;
string comments;
char comma_holder;

int main(){
    inFile.open("Matinong_Car_Data.csv");
    linkedCategories hatdog;

    while (true){
        //Get All Items
        cout << "Reading from file------------/" << endl;
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

        if (category != prev_category){
            //cout << "New category found, making a new one----////" << endl;
            //cout << "Category Name of Previous " << prev_category 
                 //<< " | New Category Name: " << category << endl << endl;
            prev_category = category;
            hatdog.add_new_category(category);
            //cout << "Category " << category << " was added" << endl;
        }

        //cout << "Adding a new item to the category" << endl;
        hatdog.add_new_item(category, testItem);   
    }
    //cout << endl << endl << endl;
    //cout << "Done Inputting" << endl;
    //cout << "Will only output Category: SUV" << endl;
    //hatdog.printCategory("SUV");
    //hatdog.printAll();
    //string a;
    //cin >> a;

    return 0;
}