#ifndef H_modifiedUnorderedArrayListType
#define H_modifiedUnorderedArrayListType

#include <iostream>
#include "modifiedArrayList.h"

template <class Type>
class modifiedUnorderedArrayListType : public modifiedArrayListType<Type> {
  public:
    void insertAt(int location,  Type& item);
    void insertEnd( Type& item);
    void replaceAt(int location,  Type& item);
    int seqSearch(Type& item, std::string criteria);
    void remove( Type& item);
    Type getAt(int location);

    modifiedUnorderedArrayListType(int size = 100);

    
  /* Pangreference ko lang to
  protected:
    Type *list; 
    int maxSize;
    int length;
  */
};

template <class Type>
modifiedUnorderedArrayListType<Type>::modifiedUnorderedArrayListType(int size):modifiedArrayListType<Type>(size) {}

template <class Type>
void modifiedUnorderedArrayListType<Type>::insertAt(int location,  Type& item)
{
  if (location < 0 || location >= this->maxSize){
    cout << "Index out of range" << endl;
  } else if ( this->length >= this->maxSize ){
    cout << "Cannot insert into a full list" << endl;
  } else {
    for (int i = this->length; i > location; i--){
      this->list[i] = this->list[i-1];
    }

    this->list[location] = item;
    this->length++;
  }
}

template <class Type>
void modifiedUnorderedArrayListType<Type>::insertEnd( Type& item) 
{
  if (this->length == this->maxSize){
    cout << "List is already full" << endl;
  } else {
    this->list[this->length] = item;
    this->length++;
  }
}

template <class Type>
void modifiedUnorderedArrayListType<Type>::replaceAt(int location,  Type& item)
{
  if (location < 0 || location > this->maxSize) {
    cout << "location is out of bounds" << endl;
  } else {
    this->list[location] = item;
  }
}

template <class Type>
int modifiedUnorderedArrayListType<Type>::seqSearch(Type& item, std::string criteria) 
{
  int result = -1;
  if (criteria == "name"){
    for (int i = 0; i < this->length; i++){
      if (this->list[i].get_name_of_item() == item.get_name_of_item()){
        result = i;
        break;
      }
    }
  } else if (criteria == "brand"){
    for (int i = 0; i < this->length; i++){
      if (this->list[i].get_brand() == item.get_brand()){
        result = i;
        break;
      }
    }
  } else if (criteria == "category"){
    for (int i = 0; i < this->length; i++){
      if (this->list[i].get_category() == item.get_category()){
        result = i;
        break;
      }
    }
  } else if (criteria == "quantity"){
    for (int i = 0; i < this->length; i++){
      if (this->list[i].get_quantity() == item.get_quantity()){
        result = i;
        break;
      }
    }
  } else if (criteria == "cost") {
    for (int i = 0; i < this->length; i++){
      if (this->list[i].get_cost() == item.get_cost()){
        result = i;
        break;
      }
    }
  } else if (criteria == "comments") { //Will detect in a long line of string
    for (int i = 0; i < this->length; i++){
      if (this->list[i].get_comments() == item.get_comments()){
        result = i;
        break;
      }
    }
  } else {
    for (int i = 0; i < this->length; i++){
      if (this->isItemAtEqual(i, item)){
        result = i;
        break;
      }
    }
  }
  
  return result;
}

template <class Type>
void modifiedUnorderedArrayListType<Type>::remove( Type& item){
  int found = seqSearch(item, "name");
  if (this->length == 0){
    cout << "List is empty" << endl;
  } else if (found == -1){
    cout << "Item not found" << endl;
  } else {
    this->removeAt(found);
  }
}

template <class Type>
Type modifiedUnorderedArrayListType<Type>::getAt(int location) {
  Type result;
  if (location > this->length || location < 0){
    cout << "Invalid Location";
  } else {
    result = this->list[location];
  }

  return result;
}

#endif