export const getDailyGames = (name: string) => {
  var currentDate = new Date();
  const data = fetch(
    `https://localhost:7242/api/Games?name=${name}&year=${currentDate.getFullYear()}&month=${
      currentDate.getMonth() + 1
    }&day=${currentDate.getDate()}`
  )
    .then((response) => response.json())
    .then((data) => {
      return data;
    });

  console.log(data);
};
