#ifndef H_linkedCategoriesType
#define H_linkedCategoriesType

#include <iostream>
#include <iomanip>
#include <fstream>
#include "ItemType.h"
#include "ItemListType.h"
#include "CategoryType.h"
#include "modifiedUnorderedLinkedList.h"

using namespace std;

class linkedCategories : public unorderedLinkedList<CategoryType>
{
  public:

    void add_new_category(CategoryType newCat);
    void add_new_category(string category_name, string category_description = "No available description");
 

    void delete_category(string delCat_name);
    void delete_category(CategoryType delCat);

    void printCategory(string printCat_name);

    void showItem(CategoryType category, int location);

    void add_new_item(string category, Item newItem);
    void add_new_item(Item newItem);

    void printAll(int step = 25);
    void print_available_categories();

    void searchFor(Item searchItem);

    void modifyItem(Item editItem);

    void sortBy(int criteria);

    string BigListContents();

    linkedCategories();
    linkedCategories(int size = 100);
  private:
    int category_capacity;
    string category;
    int location;
    nodeType<CategoryType>* current;

};

void linkedCategories::add_new_item(string category, Item newItem){
  
  current = this->first; 

  while (current != NULL){   
      if (current->info.get_name() == category){
        current->info.add_to_total_quantity(newItem.get_quantity());
        current->info.add_new_item(newItem);
        break;
      } else {
        current = current->link;
      }
  }  
}

void linkedCategories::add_new_item(Item newItem){
  
  current = this->first; 
  bool added = false;
  while (current != NULL){
      if (current->info.get_name() == newItem.get_category()){
        current->info.add_to_total_quantity(newItem.get_quantity());
        current->info.add_new_item(newItem);
        added = true;
        break;
      } else {
        current = current->link;
      }
  }

  if (!added)  {
    this->add_new_category(newItem.get_category());
    this->add_new_item(newItem);
  }
}

void linkedCategories::add_new_category(string category_name, string category_description){
  CategoryType newCategory(category_name, category_description, 0, this->category_capacity);
  this->insertLast(newCategory);
}

void linkedCategories::add_new_category(CategoryType newCat)
{
  this->insertLast(newCat);
}

void linkedCategories::delete_category(CategoryType delCat)
{
  this->deleteNode(delCat);
}

void linkedCategories::delete_category(string delCat_name){
  CategoryType deleteCategory(delCat_name, " ", 0, 0);
  this->deleteNode(deleteCategory);
}

void linkedCategories::printCategory(string printCat_name)
{
  nodeType<CategoryType>* current; 
  current = this->first; 
  int step = 20;
  while (current != NULL)   
      if (current->info.get_name() == printCat_name){
        cout << endl
             << "Category Name : " << current->info.get_name() << endl
             << "Total Quantity of Items in this Category: " << current->info.get_total_quantity() << endl
             << endl;
        cout << "Category List Contents" << endl;
        cout << setw(step) <<  setfill(' ')  << "Category"
             << setw(step+1) << "Brand"
             << setw(step+1) << "Name"
             << setw(step+1) << "Quantity"
             << setw(step+1) << "Cost"
             << setw(step+5) << "Comments"
             << endl;
        current->info.print_items();

        break;
      } 
          
      else
          current = current->link; 
}

void linkedCategories::printAll(int step){
  nodeType<CategoryType>* current; 
  current = this->first; 
  cout << setw(step) <<  setfill(' ')  << "Category"
                << setw(step+1) << "Brand"
                << setw(step+1) << "Name"
                << setw(step+1) << "Quantity"
                << setw(step+1) << "Cost"
                << setw(step+5) << "Comments"
                << endl;
  while (current != NULL){
    current->info.print_items(step);
    current = current->link;
  }   
    
}

linkedCategories::linkedCategories(int size){
  if (size < 0) {
    this->category_capacity = 100;
  } else {
    this->category_capacity = size;
  }
}

void linkedCategories::searchFor(Item searchItem){
  
  nodeType<CategoryType> *current;
  current = this->first; 

  ItemListType *searchResults;

  bool has_category = false;
  bool has_name = false;
  bool has_brand = false;
  bool has_description = false;
  bool has_quantity = false; 
  bool has_cost = false;
  int step = 20;

  has_category = (searchItem.get_category().length() > 1);
  has_name = (searchItem.get_name_of_item().length() > 1);
  has_brand = (searchItem.get_brand().length() > 1);
  has_description = (searchItem.get_comments().length() > 1);
  has_quantity = (searchItem.get_quantity() > 0);
  has_cost = (searchItem.get_cost() > 0);
  
  
  cout << setw(step) <<  setfill(' ')  << "Category"
                << setw(step+1) << "Brand"
                << setw(step+1) << "Name"
                << setw(step+1) << "Quantity"
                << setw(step+1) << "Cost"
                << setw(step+5) << "Comments"
                << endl;
  if (has_category){
    
    while (current != NULL)  {
      if (current->info.get_name() == searchItem.get_category()){
        searchResults = current->info.lookForItemInList(searchItem, 
                        has_name, has_brand, has_description, has_quantity, has_cost);
        break;
      } else {
        current = current -> link;
      }
    }

    searchResults->print();
  } else {
    while (current != NULL) {
      searchResults = current->info.lookForItemInList(searchItem, 
                        has_name, has_brand, has_description, has_quantity, has_cost);
      searchResults->print();
      current = current -> link;
    
    }
  }
}

void linkedCategories::sortBy(int criteria){

  int temp_capacity = this->length() * this->category_capacity;
  ItemListType *tempSortedList;
  tempSortedList = new ItemListType(temp_capacity);
  ItemListType *temporaryList;
  temporaryList = new ItemListType(this->category_capacity);
  Item tempItem;
  int step = 20;
  
  string sort_criteria;
  switch (criteria){
    case 1:
      sort_criteria = "Names";
      break;
    case 2:
      sort_criteria = "Brand";
      break;
    case 3:
      sort_criteria = "Quantity";
      break;
    case 4:
      sort_criteria = "Cost";
      break;
    case 5:
      sort_criteria = "Comments";
      break;
    case 6:
      sort_criteria = "Category";
      break;
    default:
      break;
  }
  
  current = this->first;
  while (current != NULL){
    temporaryList = current->info.getList();
    for (int i = 0; i < temporaryList->listSize(); i++){
      tempItem = temporaryList->getAt(i);
      tempSortedList->insertEnd(tempItem);
    }
    current = current->link;
  }

  tempSortedList->sort(sort_criteria);

  cout << setw(step) <<  setfill(' ')  << "Category"
                << setw(step+1) << "Brand"
                << setw(step+1) << "Name"
                << setw(step+1) << "Quantity"
                << setw(step+1) << "Cost"
                << setw(step+5) << "Comments"
                << endl;

  tempSortedList->print();

}

string linkedCategories::BigListContents(){
  string all_contents="";
  current = this->first;
  while (current != NULL) {
    all_contents += current->info.CategoryContents();
    current = current->link;
  }

  return all_contents;
}

void linkedCategories::print_available_categories(){
  current = this->first;
  while (current != NULL){
    cout << current->info.get_name() << endl;
    current = current -> link;
  }
}

void linkedCategories::modifyItem(Item editItem){

  current = this->first; 

  ItemListType *searchResults;
  Item tempItem;
  int modify_choice;

  string category;
  string brand;
  string name;
  string description;
  int quantity;
  double cost;

  bool has_category = false;
  bool has_name = false;
  bool has_brand = false;
  bool has_description = false;
  bool has_quantity = false; 
  bool has_cost = false;
  bool should_be_added = true;

  int step = 20;

  has_category = (editItem.get_category().length() > 1);
  has_name = (editItem.get_name_of_item().length() > 1);
  has_brand = (editItem.get_brand().length() > 1);
  has_description = (editItem.get_comments().length() > 1);
  has_quantity = (editItem.get_quantity() > 0);
  has_cost = (editItem.get_cost() > 0);
  
  
  cout << setw(step) <<  setfill(' ')  << "Category"
                << setw(step+1) << "Brand"
                << setw(step+1) << "Name"
                << setw(step+1) << "Quantity"
                << setw(step+1) << "Cost"
                << setw(step+5) << "Comments"
                << endl;

  while (current != NULL)  {
    if (current->info.get_name() == editItem.get_category()){
      searchResults = current->info.lookForItemInList(editItem, 
                      has_name, has_brand, has_description, has_quantity, has_cost);
      tempItem = searchResults->getAt(0);

      current->info.remove_item(tempItem);
      int subtract = tempItem.get_quantity() * -1;
      current->info.add_to_total_quantity(subtract);
      break;
    } else {
      current = current -> link;
    }
  }

  searchResults->print();

  cout << "What would you like to change for this item?" << endl;
  cout << "[1] Name" << endl;
  cout << "[2] Brand" << endl;
  cout << "[3] Category" << endl;
  cout << "[4] Quantity" << endl;
  cout << "[5] Cost" << endl;
  cout << "[6] Description" << endl;
  cin >> modify_choice;

  switch (modify_choice){
    case 1:
      cout << "Name: " << endl;
      cin.ignore();
      getline(cin, name);
      tempItem.set_name_of_item(name);
      break;
    case 2:
      cout << "Brand: " << endl;
      cin.ignore();
      getline(cin, brand);
      tempItem.set_brand(brand);
      break;
    case 3:
      cout << "Category: " << endl;
      cin.ignore();
      getline(cin, category);
      tempItem.set_category(category);
      break;
    case 4:
      cout << "Quantity " << endl;
      cin >> quantity;
      if (quantity <= 0)
        should_be_added = false;
      tempItem.set_quantity(quantity);
      break;
    case 5:
      cout << "Cost " << endl;
      cin >> cost;
      tempItem.set_cost(cost);
      break;
    case 6:
      cout << "Description: " << endl;
      cin.ignore();
      getline(cin, description);
      tempItem.set_comments(description);
      break;
    default:
      cout << "Invalid Choice" << endl;
      break;
  }
  if (should_be_added)
    this->add_new_item(tempItem);
}

#endif