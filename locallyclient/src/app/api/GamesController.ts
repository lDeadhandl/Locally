export const getDailyGames = async (name: string) => {
  var currentDate = new Date();
  const data = await fetch(
    `https://localhost:7242/api/Games?name=${name}&year=${currentDate.getFullYear()}&month=${
      currentDate.getMonth() + 1
    }&day=${currentDate.getDate()}`
  )
    .then((response) => response.json())
    .then((data) => {
      return data;
    });

  return data;
};
