#ifndef H_CategoryType
#define H_CategoryType

#include <fstream>
#include <string>

#include "ItemListType.h"
#include "ItemType.h"

using namespace std;

class CategoryType{
  public:
    string get_name() const;

    int get_total_quantity();


    void set_name(string n);

    void set_total_quantity(int tq);
    

    void copy_list(ItemListType otherList);

    void add_to_total_quantity(int add);
    void add_new_item(Item addItem);

    void remove_item(Item delItem);

    void print_items(int step = 20);

    CategoryType();
    CategoryType(int size);
    CategoryType(string n, string d, int tq, int size = 100);

    void sortBy(string criteria);

    string CategoryContents();

    ItemListType *lookForItemInList(Item searchItem, bool has_name, bool has_brand, bool has_description, bool has_quantity, bool has_cost);
    ItemListType *getList();

  protected:
    string name;
    int total_quantity;
    ItemListType* all_items;    
};
ItemListType *CategoryType::getList() {
  return this->all_items;
}

string CategoryType::get_name() const
{
  return this->name;
}

int CategoryType::get_total_quantity()
{
  return this->total_quantity;
}

void CategoryType::set_name(string n)
{
  this->name = n;
}

void CategoryType::set_total_quantity(int tq)
{
  this->total_quantity = tq;
}

void CategoryType::add_to_total_quantity(int add){
  this->total_quantity += add;
}

CategoryType::CategoryType() {}

CategoryType::CategoryType(string n, string d, int tq, int size)
{
  this->name = n;
  this->total_quantity = tq; 


  if (size < 0)
    size = 100;
  
  all_items = new ItemListType(size);
}

CategoryType::CategoryType(int size)
{
  if (size < 0)
    size = 100;
  all_items = new ItemListType(size);
}

void CategoryType::print_items(int step){
  this->all_items->print(step);
}

void CategoryType::copy_list(ItemListType otherList){
  Item otherItem;
  int other_size = otherList.listSize();
  for (int i = 0; i < other_size; i++){
    otherItem.set_Details(otherList.getAt(i));
    this->all_items->insertEnd(otherItem);
  }
}

void CategoryType::add_new_item(Item addItem){
  this->all_items->insertEnd(addItem);
}

void CategoryType::sortBy(string criteria){
  this->all_items->sort(criteria);
}

ItemListType *CategoryType::lookForItemInList(Item searchItem, bool has_name, bool has_brand,bool has_description,bool has_quantity,bool has_cost) {
  ItemListType *resultingList;
  resultingList = new ItemListType((this->all_items->maxListSize()));
  Item otherItem;

  string name = searchItem.get_name_of_item();
  string brand = searchItem.get_brand();
  int quantity = searchItem.get_quantity();
  double cost = searchItem.get_cost();
  string comments = searchItem.get_comments();
  int length = this->all_items->listSize();

  for (int i = 0; i < length; i++){
    otherItem.set_Details(this->all_items->getAt(i));
    resultingList->insertEnd(otherItem);
  }
  
  if (has_quantity && !resultingList->isEmpty()){
    resultingList->sort("Quantity");
    resultingList->removeExcept("Quantity", quantity, 0);
  }


  if (has_cost && !resultingList->isEmpty()){
    resultingList->sort("Cost");
    resultingList->removeExcept("Cost", 0, cost);
  }

  if (has_name && !resultingList->isEmpty()) {
    resultingList->sort("Names");
    resultingList->removeExcept("Name", 0, 0, name);
  }

  if (has_brand && !resultingList->isEmpty()) {
    resultingList->sort("Brand");
    resultingList->removeExcept("Brand", 0, 0, brand);
  }

  if (has_description && !resultingList->isEmpty()){
    resultingList->sort("Comments");
    resultingList->removeExcept("Comments", 0, 0, comments);
  }
  
  return resultingList;
}

string CategoryType::CategoryContents(){
  string result="";
  for (int i = 0; i < this->all_items->listSize(); i++){
    result += this->all_items->get_category_at(i);
    result += ',';
    result += this->all_items->get_brand_at(i);
    result += ',';
    result += this->all_items->get_name_at(i);
    result += ',';
    result += to_string(this->all_items->get_quantity_at(i));
    result += ',';
    result += to_string(this->all_items->get_cost_at(i));
    result += ',';
    result += this->all_items->get_comments_at(i);
    result += '\n';
  }
  return result;
}

void CategoryType::remove_item(Item delItem){
  this->all_items->remove(delItem);
}

#endif