#ifndef H_modifiedArrayList
#define H_modifiedArrayList

#include <iostream>
#include <iomanip>

using namespace std;

template <class Type>
class modifiedArrayListType {
  public:
    bool isEmpty(); //Check if List is Empty 
    bool isFull(); //Check if List is Full 
    int listSize(); //Return the length 
    int maxListSize(); //Return the maxListSize
    bool isItemAtEqual(int location,  Type& item); //Check if Item is equal to specified
    void removeAt(int location); //Remove the item 
    void clearList(); //Clears the list 
    void print(int step = 20);

    modifiedArrayListType(int size = 100);
    modifiedArrayListType( modifiedArrayListType<Type>& otherList);

    virtual void insertAt(int location,  Type& item) = 0;
    virtual void insertEnd( Type& item) = 0;
    virtual void replaceAt(int location,  Type& item) = 0;
    virtual int seqSearch( Type& item, string criteria) = 0;
    virtual void remove( Type& item) = 0;
    virtual ~modifiedArrayListType();
  
  protected:
    Type *list;
    int maxSize;
    int length;

};

template <class Type>
void modifiedArrayListType<Type>::print(int step){
  for (int i = 0; i < length; i++){
    std::cout << std::fixed << std::setprecision(2) << setw(step) << setfill(' ')
         << this->list[i].get_category() << "|" << setw(step) 
         << this->list[i].get_brand() << "|" << setw(step) 
         << this->list[i].get_name_of_item() << "|" << setw(step) 
         << this->list[i].get_quantity() << "|" << setw(step) 
         << this->list[i].get_cost() << "| Details: " << setw(step) 
         << this->list[i].get_comments() << endl;
  }
}
template <class Type>
bool modifiedArrayListType<Type>::isEmpty()
{
  return (this->length == 0);
}

template <class Type>
bool modifiedArrayListType<Type>::isFull()
{
  return (this->length == this->maxSize);
}

template <class Type>
int modifiedArrayListType<Type>::listSize()
{
  return (this->length);
}

template <class Type>
int modifiedArrayListType<Type>::maxListSize()
{
  return (this->maxSize);
} 

template <class Type>
bool modifiedArrayListType<Type>::isItemAtEqual(int location,  Type& item)
{
  bool r1 = (this->list[location].get_name_of_item() == item.get_name_of_item());
  bool r2 = (this->list[location].get_brand() == item.get_brand());
  bool r3 = (this->list[location].get_category() == item.get_category());
  bool r4 = (this->list[location].get_cost() == item.get_cost());
  bool r5 = (this->list[location].get_quantity() == item.get_quantity());

  return (r1 * r2 * r3 * r4 * r5);
}


template <class Type>
void modifiedArrayListType<Type>::removeAt(int location)
{
  if (this->length == 0)
  {
    cout << "Cannot remove from an empty list " << endl;
  } else if(location > this->length || location < 0){
    cout << "Invalid index location" << endl;
  } else {
    for (int i = location; i < this->length-1; i++){
      this->list[i] = this->list[i+1];
    }

    this->length--;
  }
}

template <class Type>
void modifiedArrayListType<Type>::clearList(){
  this->length = 0;
} 

template <class Type>
modifiedArrayListType<Type>::modifiedArrayListType(int size){
  if (size < 0){
    this->maxSize = 100;
  } else {
    this->maxSize = size;
  }

  this->length = 0;
  list = new Type[this->maxSize];

}

template <class Type>
modifiedArrayListType<Type>::modifiedArrayListType( modifiedArrayListType<Type>& otherList)
{
  this->maxSize = otherList.maxListSize();
  this->length = otherList.listSize();
  for (int i = 0; i < length; i++){
    this->list[i] = otherList.list[i];
  }
}

template <class Type>
modifiedArrayListType<Type>::~modifiedArrayListType(){
  delete [] this->list;
}


#endif

