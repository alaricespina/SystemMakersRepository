#include <iostream>
#include <stdlib.h>
#include <fstream>
#include <string>
#include <iomanip>

#include "modifiedArrayList.h"
#include "ItemType.h"
#include "linkedCategories.h"

using namespace std;


//Function Prototypes
void mainMenu();
void viewItems();
void sortItems();
void loadNewFile();
void loadFile(string filename_with_ext);
void saveFile(string filename_with_ext);
void searchForItem();
void linebreak(char fill = '-');
void backToMainMenu();
void depositItem();
void withdrawItem();

//Function printing Prototypes
void centerText(string text, char fill= '-');
void rightText(string text, char fill = ' ');
void leftText(string text, char fill = ' ');


//Variables
int screen_width = 120;
int choice = 1;
string extension = ".csv";
string filename_with_ext;
string filename = " "; //FileName to be loaded
ifstream infile; //CSV File containing data
ifstream savedfilename;
ofstream newfilenamesave;
ofstream csvSaveFile;

linkedCategories BigList(100);

int main() {
  system("CLS"); //if on Replit CLS, if VScode CLS

  savedfilename.open("SaveFilePrevRun.txt");

  getline(savedfilename, filename);
  
  filename_with_ext = filename + extension;
  savedfilename.close();
  

  //MainLoop
  while (choice > 0 && choice <= 6){
    mainMenu(); //Print the welcome and valid options
    cin >> choice;
    system("CLS");
    if (choice > 6 || choice < 0)
      cout << "Invalid Choice." << endl; 
    else {
      switch (choice) {
        case 1:
          viewItems();
          
          system("CLS");
          break;
        case 2:
          sortItems();
          
          break;
        case 3:
          loadNewFile();
          system("CLS");
          break;
        case 4:
          searchForItem();
          system("CLS");
          break;
        case 5:
          depositItem();
          
          system("CLS");
          break;
        case 6:
          withdrawItem();
          
          system("CLS");
        case 0:
          system("CLS");

          break;
      }
    }
  }
  saveFile(filename_with_ext);
  linebreak();
  centerText("Exiting the program", ' ');
  centerText("Thank you for using the Inventory System", ' ');
  centerText("From Group MayaNa", ' ');
  linebreak();

  return 0;
}

void centerText(string text, char fill){
  int text_length = text.length();
  int half_adjuster = text_length/2;
  int half_screen = screen_width/2;
  int half_length = half_screen - half_adjuster;
  cout << fixed << right << setw(half_length) << setfill (fill) << fill //Left Side
       << setw(text_length) << setfill(fill) << text; // Text Itself
  if (text_length % 2 != 0){ //Right Side (Adjusting)
    cout << setw(half_length-1) << setfill (fill) << fill<< endl;
  } else {
    cout << setw(half_length) << setfill (fill) << fill << endl;
  }
       
}

void rightText(string text, char fill){
  cout << fixed << right << setw(screen_width) << setfill(fill) << text << endl;
}

void leftText(string text, char fill){
  cout << fixed << left << setw(screen_width) << setfill(fill) << text << endl;
}

void mainMenu(){

  centerText("Inventory System");
  rightText("By Group Maya Na");
  rightText("Update: 8-02-2021");
  linebreak();
  rightText("Current Loaded File: " + filename_with_ext);
  centerText("Valid Options");
  leftText("[1] VIEW Items"); 
  leftText("[2] SORT Items"); 
  leftText("[3] LOAD a different Save File"); 
  leftText("[4] SEARCH for an item"); 
  leftText("[5] DEPOSIT an Item");
  leftText("[6] WITHDRAW an Item");
  leftText("[0] EXIT");
  linebreak();
  if (filename.length() < 3){
    linebreak('#');
    centerText("  WARNING  ",' ');
    centerText("No File specified to be loaded, please load a file first", ' ');
    linebreak('#');
    linebreak();
  } else {
    loadFile(filename_with_ext);
  }
  cout << "Enter the number of your choice: " << endl;
}

void backToMainMenu(){
  cout << endl << "Press any key to continue";
  cin.get();
  system("CLS");
}

void viewItems() {
  int viewChoice;
  int operationChoice;
  string category_name;

  centerText("View Items");
  cout << "How would you like to view the items?" << endl;
  cout << "[1] All Items" << endl << "[2] By Category" << endl;
  linebreak();
  cin >> viewChoice;
  
  switch (viewChoice){
    case 1:
      system("CLS");
      centerText("Viewing All Items");     

      BigList.printAll();
      break;
    case 2:
      cout << "Enter the category that you want to view: " << endl;
      cin.ignore();
      getline(cin, category_name);
      system("CLS");  
      centerText("Viewing Category");
      cout << "Category: " << category_name << endl;
      linebreak();
      
      BigList.printCategory(category_name);
      break;
    default:
      break;
  }

  centerText("Additional Operations");
  cout << "[1] Deposit an Item" << endl;
  cout << "[2] Withdraw an Item" << endl;
  cout << "[3] Back to Main Menu" << endl;
  linebreak();
  cout << "Enter your choice: ";
  cin >> operationChoice;
  switch (operationChoice) {
    case 1:
      depositItem();
      break;
    case 2:
      withdrawItem();
      break;
    case 3:
      cout << "Returning to Main Menu" << endl;
      break;
    default:
      break;
  } 

  backToMainMenu();
}

void sortItems() {
  int sortchoice;
  cout << "------------Sort Items------------" << endl;
  cout << "How do you want to sort the items:" << endl;
  linebreak();
  cout << "Note: All options are in ascending order (A-Z) & (0-9)" << endl;
  cout << "Available options: " << endl;
  linebreak();
  cout << "[1] By Name" << endl;
  cout << "[2] By Brand" << endl;
  cout << "[3] By Quantity" << endl;
  cout << "[4] By Price" << endl;
  cout << "[5] By Description" << endl;
  cout << "[6] By Category" << endl;
  linebreak();
  cin >> sortchoice;

  BigList.sortBy(sortchoice);

  cin.ignore();
  backToMainMenu();

}

void loadNewFile() {
  newfilenamesave.open("SaveFilePrevRun.txt", ios::trunc);
  int tempchoice;

  while (tempchoice != 1 && tempchoice != 3){
    centerText("Load File");
    cout << "Current File: "  << filename << endl;
    linebreak();
    cout << "Instruction: " << endl;
    cout << "Input the filename to be loaded" << endl;
    linebreak();
    cin >> filename;
    filename_with_ext = filename + extension;
    linebreak();
    centerText("  CONFIRMATION  ", '#');
    cout << "Please confirm that the filename is correct:" << endl << endl;
    
    cout << filename_with_ext << endl << endl;
    
    cout << "[1] Confirm" << endl << "[2] Re-enter" << endl << "[3] Back to Main Menu" << endl;
    linebreak('#');
    linebreak();
    cin >> tempchoice;
    system("CLS");
  }

  if (tempchoice != 3){
    loadFile(filename_with_ext);
  }
  
  newfilenamesave << filename;
  newfilenamesave.close();
  cin.ignore();
  backToMainMenu();
}

void searchForItem() {
  Item NewItem;
  string name_of_item;
  string brand;
  string category;
  int quantity;
  double cost;
  string comments;

  centerText("Deposit Item");
  cout << "Instructions: " << endl;
  cout << "Enter the details of the item you want to look for" << endl;
  cout << "Put 0 in the quantity or cost if you are unsure of the details" << endl;
  cout << "Leave a blank on the name or brand or category or description if you are also unsure of them" << endl;
  linebreak();
  cin.ignore();
  cout << "Name of Item: ";
  getline(cin, name_of_item);
  cout << "Brand of Item: ";
  getline(cin, brand);
  cout << "Category: ";
  getline(cin, category);
  cout << "Quantity: ";
  cin >> quantity;
  cout << "Cost: ";
  cin >> cost;
  cout << "Description / Comments: ";
  cin.ignore();
  getline(cin, comments);

  system("CLS");
  centerText("Search Results");
  NewItem.set_Details(name_of_item, brand, category, quantity, cost, comments);
  BigList.searchFor(NewItem);

  backToMainMenu();
}

void loadFile(string filename_with_ext){
  string name;
  string brand;
  string category;
  string prev_category = " ";
  int quantity;
  double cost;
  string comments;
  char comma_holder;

  Item testItem;

  if (!BigList.isEmptyList()){
    BigList.initializeList();
  }

  try {
    infile.open(filename_with_ext);
    if (!infile.is_open()){
      throw(nullptr);
    }

    while (true){
        
        getline(infile, category, ',');
        getline(infile, brand, ',');
        getline(infile, name, ',');
        infile >> quantity;
        infile >> comma_holder;
        infile >> cost;
        infile >> comma_holder;
        getline(infile, comments, '\n');
        testItem.set_Details(name, brand, category, quantity, cost, comments);
        
        if (infile.eof())
            break;

        if (category != prev_category){
            prev_category = category;
            BigList.add_new_category(category);
        }

        BigList.add_new_item(category, testItem);   
    }
    
    infile.close();
  } catch (...) {
    linebreak('#');
    centerText("An Error has occured while opening the file", ' ');
    linebreak('#');
  }
}

void linebreak(char fill){
  cout << fixed << setw(screen_width) << setfill(fill) << fill << endl;
}

void depositItem(){
  Item NewItem;
  string name_of_item;
  string brand;
  string category;
  int quantity;
  double cost;
  string comments;

  centerText("Deposit Item");
  cout << "Instructions: " << endl;
  cout << "Enter the details of the item you want to add" << endl;
  linebreak();
  cout << "Name of Item: ";
  cin.ignore();
  getline(cin, name_of_item);
  cout << "Brand of Item: ";
  getline(cin, brand);
  cout << "Category: ";
  getline(cin, category);
  cout << "Quantity: ";
  cin >> quantity;
  cout << "Cost: ";
  cin >> cost;
  cout << "Description / Comments: ";
  cin.ignore();
  getline(cin, comments);

  NewItem.set_Details(name_of_item, brand, category, quantity, cost, comments);
  BigList.add_new_item(NewItem);

  backToMainMenu();
}

void withdrawItem(){
  Item modifyItem;

  string category_name;
  string modify_name;
  string modify_brand;
  centerText("Withdraw Items");
  cout << "You can only withdraw from the following categories" << endl;
  cout << "Select a category" << endl;
  linebreak();
  BigList.print_available_categories();
  linebreak();
  cin.ignore();
  getline(cin, category_name);
  linebreak();
  system("CLS");
  cout << "Here is the list of items on the said category" << endl;
  BigList.printCategory(category_name);
  linebreak();
  cout << "Enter the brand and name of the item you want to modify" << endl;
  linebreak();
  cout << "Brand: ";
  getline(cin, modify_brand);
  cout << "Name: ";
  getline(cin, modify_name);

  modifyItem.set_Details(modify_name, modify_brand, category_name, 0, 0, "");
  BigList.modifyItem(modifyItem); 
  
}

void saveFile(string filename_with_ext){
  csvSaveFile.open(filename_with_ext, ios::trunc);
  string all;
  all = BigList.BigListContents();
  csvSaveFile << all;
  csvSaveFile.close();
}

