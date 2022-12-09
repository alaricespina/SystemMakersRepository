const FetchedData = [];
async function getData(){
  let Take = await fetch('http://jservice.io/api/clues?value=100&min_date=1985-02-20');
  let After = await Take.json();
  return Promise.all([After]);
}

async function ShowData(){
  let RawData = await getData();
  for (let objects of RawData){
  FetchedData.push(objects)};
  console.log(FetchedData);
}
