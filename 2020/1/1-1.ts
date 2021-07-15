export {}

const result = Deno
    .readTextFileSync("input")
    .split("\n")
    .map(line => parseInt(line))
    .filter((line,index,array) => 
        array.filter(compareLine => line + compareLine == 2020).length > 0);

if (result.length >= 2) console.log(result[0] * result[1]);
console.log(result);