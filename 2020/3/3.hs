-- convert input to a list of lists of terrain
-- convert list of lists to list of infinite lists
-- convert list of lists to a list of what gets hit
-- count bush from this list

main :: IO ()
main = do
    input <- getContents
    let inputLines = lines input
    let infInputLines = makeStringsInfinite inputLines
    let hitTiles = filterToHitTiles infInputLines
    let trees = getOccuranceNo '#' hitTiles
    print trees

makeStringsInfinite :: [String] -> [String]
makeStringsInfinite list = [ cycle str | str <- list ]

filterToHitTiles :: [String] -> [Char]
filterToHitTiles field = [ field!!y!!x | x <- [1..(length field - 1) * 3], y <- [1..length field-1], x == 3 * y]

getOccuranceNo :: Eq a => a -> [a] -> Int
getOccuranceNo _ [] = 0
getOccuranceNo check (x:xs)
    | check == x = 1 + getOccuranceNo check xs
    | otherwise = getOccuranceNo check xs