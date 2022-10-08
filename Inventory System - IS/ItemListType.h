#ifndef H_ItemListType 
#define H_ItemListType

#include <iostream>
#include "ItemType.h"
#include "modifiedUnorderedArrayList.h"

class ItemListType : public modifiedUnorderedArrayListType<Item>{
  public:
    std::string get_name_at(int location);
    std::string get_brand_at(int location);
    std::string get_category_at(int location);
    int get_quantity_at(int location);
    double get_cost_at(int location);
    std::string get_comments_at(int location);

    void set_name_at(std::string name, int location);
    void set_brand_at(std::string brand, int location);
    void set_category_at(std::string category, int location);
    void set_quantity_at(int quantity, int location);
    void set_cost_at(double cost, int location);
    void set_comments_at(std::string comments, int location);

    void sort(string criteria);

    void removeExcept(std::string criteria, int quantity = 0, double cost = 0.00, std::string holder = "");
    ItemListType(int size = 100);

  protected:
    void selectionSort(std::string crtieria);
};

void ItemListType::set_name_at(std::string name, int location) {
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    this->list[location].set_name_of_item(name);
  }
}

void ItemListType::set_brand_at(std::string brand, int location) {
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    this->list[location].set_brand(brand);
  }
}

void ItemListType::set_category_at(std::string category, int location) {
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    this->list[location].set_category(category);
  }
}

void ItemListType::set_comments_at(std::string comments, int location) {
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    this->list[location].set_comments(comments);
  }
}

void ItemListType::set_cost_at(double cost, int location) {
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    this->list[location].set_cost(cost);
  }
}

void ItemListType::set_quantity_at(int quantity, int location){
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    this->list[location].set_quantity(quantity);
  }
}

std::string ItemListType::get_name_at(int location) {
    std::string result;
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    result = this->list[location].get_name_of_item();
  }
  return result;
}

std::string ItemListType::get_brand_at(int location) {
  std::string result;
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    result = this->list[location].get_brand();
  }
  return result;
}

std::string ItemListType::get_category_at(int location) {
  std::string result;
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    result = this->list[location].get_category();
  }
  return result;
}

int ItemListType::get_quantity_at(int location) {
  int result;
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    result = this->list[location].get_quantity();
  }
  return result;
}

double ItemListType::get_cost_at(int location) {
  double result;
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    result = this->list[location].get_cost();
  }
  return result;
}

std::string ItemListType::get_comments_at(int location){
  std::string result;
  if (location < 0 || location > this->maxListSize()){
    cout << "Invalid location" << endl;
  } else {
    result = this->list[location].get_comments();
  }
  return result;
}

ItemListType :: ItemListType(int size) : modifiedUnorderedArrayListType(size)
{
}

void ItemListType::sort(std::string criteria){
  if (!(criteria == "Quantity" || criteria == "Cost" || criteria == "Names" || criteria == "Brand" || criteria == "Comments" || criteria == "Category")){
    cout << "Invalid Criterion for sorting" << endl;
  } else {
    selectionSort(criteria);
  }
  
}

void ItemListType::removeExcept(std::string criteria, int quantity, double cost, std::string holder){
  ItemListType* temp;
  temp = new ItemListType(this->listSize());
  Item newItem;

  bool cquantity, ccost, cname, cbrand, ccomments, compare_results;

  for (int i = 0; i < this->listSize(); i++){
    cquantity = (criteria == "Quantity") && (this->list[i].get_quantity() == quantity);
    ccost = (criteria == "Cost") && (this->list[i].get_cost() == cost);
    cname = (criteria == "Name") && ((this->list[i].get_name_of_item().find(holder))!=string::npos);
    cbrand = (criteria == "Brand") && ((this->list[i].get_brand().find(holder))!=string::npos);
    ccomments = (criteria == "Comments") && ((this->list[i].get_comments().find(holder))!=string::npos);
    compare_results = cquantity || ccost || cname || cbrand || ccomments;
    if (compare_results){
      newItem.set_quantity(this->list[i].get_quantity());
      newItem.set_cost(this->list[i].get_cost());
      newItem.set_name_of_item(this->list[i].get_name_of_item());
      newItem.set_brand(this->list[i].get_brand());
      newItem.set_comments(this->list[i].get_comments());
      newItem.set_category(this->list[i].get_category());
      temp->insertEnd(newItem);
    }
  }

  this->length = temp->length;
  this->list = temp->list;

}

void ItemListType::selectionSort(std::string criteria){
  int smallest = 0;
  Item tempItem;

  int length = this->listSize();
  bool ccategory, cquantity, ccost, cname, cbrand, ccomments, compare_results;
   
  for (int i = 0; i < length-1; i++)    {
        smallest = i;
        for (int j = i+1; j < length; j++){
            ccategory = (criteria == "Category") && (this->list[j].get_category() <= this->list[smallest].get_category());
            cquantity = (criteria == "Quantity") && (this->list[j].get_quantity() <= this->list[smallest].get_quantity());
            ccost = (criteria == "Cost") && (this->list[j].get_cost() <= this->list[smallest].get_cost());
            cname = (criteria == "Name") && (this->list[j].get_name_of_item() <= this->list[smallest].get_name_of_item());
            cbrand = (criteria == "Brand") && (this->list[j].get_brand() <= this->list[smallest].get_brand());
            ccomments = (criteria == "Comments") && (this->list[j].get_comments() <= this->list[smallest].get_comments());
            compare_results = ccategory || cquantity || ccost || cname || cbrand || ccomments;

            if (compare_results) {
                smallest = j;
            }
        }
        tempItem = this->list[i];
        this->list[i] = this->list[smallest];
        this->list[smallest] = tempItem;
    }
}

#endif