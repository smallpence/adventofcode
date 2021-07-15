export {}

const result = Deno
    .readTextFileSync("input")
    .split("\n")
    .map(line => parseInt(line))
    .filter(num => !isNaN(num));

function tryFindSum(allNums: number[], triedNums: number[]) {
    if (triedNums.length <= 2)
        for (let num of allNums) {
            tryFindSum(allNums.filter(testNum => testNum != num), [...triedNums, num]);
        }
    else if (triedNums.reduce((acc, cur) => acc + cur) == 2020)
        console.log(triedNums.reduce((acc, cur) => acc * cur))
}

tryFindSum(result, []);