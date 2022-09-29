#ifndef H_ItemType
#define H_ItemType 

#include <iostream>


class Item{
  public:
    std::string get_name_of_item();
    std::string get_brand();
    std::string get_category();
    int get_quantity();
    double get_cost();
    std::string get_comments();

    void set_name_of_item(std::string nameOfItem);
    void set_brand(std::string brand);
    void set_category(std::string category);
    void set_quantity(int quantity);
    void set_cost(double cost);
    void set_comments(std::string comments);
    void set_Details(std::string noi, std::string brand, std::string category, int quantity, double cost, std::string comments);
    void set_Details(Item otherItem);

    Item();
    Item(std::string noi, std::string brand, std::string category, int quantity, double cost, std::string comments);
  private:
    std::string name_of_item;
    std::string brand;
    std::string category;
    int quantity;
    double cost;
    std::string comments;

};

void Item::set_Details(Item otherItem) {
  this->name_of_item = otherItem.get_name_of_item();
  this->brand = otherItem.get_brand();
  this->category = otherItem.get_category();
  this->quantity = otherItem.get_quantity();
  this->cost = otherItem.get_cost();
  this->comments = otherItem.get_comments();

}

std::string Item::get_name_of_item(){
  return this-> name_of_item;
}

std::string Item::get_brand() {
  return this->brand;
}

std::string Item::get_category(){
  return this->category;
}

int Item::get_quantity() {
  return this->quantity;
}

double Item::get_cost() {
  return this->cost;
}

std::string Item::get_comments(){
  return this->comments;
}

void Item::set_name_of_item(std::string nameOfItem){
  this->name_of_item = nameOfItem;
}

void Item::set_brand(std::string brand){
  this->brand = brand;
}

void Item::set_category(std::string category){
  this->category = category;
}

void Item::set_quantity(int quantity){
  this->quantity = quantity;
}

void Item::set_cost(double cost){
  this->cost = cost;
}

void Item::set_comments(std::string comments){
  this->comments = comments;
}

Item::Item(std::string noi, std::string brand, std::string category, int quantity, double cost, std::string comments){
  this->name_of_item = noi;
  this->brand = brand;
  this->category = category;
  this->quantity = quantity;
  this->cost = cost;
  this->comments = comments;
}

Item::Item()
{
}

void Item::set_Details(std::string noi, std::string brand, std::string category, int quantity, double cost, std::string comments){
  this->name_of_item = noi;
  this->brand = brand;
  this->category = category;
  this->quantity = quantity;
  this->cost = cost;
  this->comments = comments;
}

#endif