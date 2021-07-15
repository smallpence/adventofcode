import Data.List

main = do 
    input <- getContents
    let x = findLargestSum(toIntList(lines input))
    let result = fst x * snd x
    print result

findLargestSum :: [Int] -> (Int, Int)
findLargestSum x = head [ (a,b) | a <- x, b <- x, a /= b , a + b == 2020]

toIntList :: [String] -> [Int]
toIntList x = [read y | y <- x]