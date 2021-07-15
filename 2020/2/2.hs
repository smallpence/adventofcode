import Data.List ()
import Text.Regex.TDFA ((=~))
import Text.Regex.TDFA.Text ()
import HaskellSay (haskellSay)

main :: IO ()
main = do
    input <- getContents
    let inputLines = map (isValidPassword . toTypedTuple . toTuples) ( lines input )
    print (getOccuranceNo True inputLines)


toTuples :: String -> (String, String, String, [String])
toTuples x = x =~ "([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)" :: (String, String, String, [String])

toTypedTuple :: (String, String, String, [String]) -> (Int, Int, Char, String)
toTypedTuple (_,_,_,[num1, num2, char:chars, password]) = (read num1, read num2, char, password)

isValidPassword :: (Int, Int, Char, String) -> Bool
isValidPassword (min, max, char, password)
    | occurances < min = False
    | occurances > max = False
    | otherwise = True
    where occurances = getOccuranceNo char password

getOccuranceNo :: Eq a => a -> [a] -> Int
getOccuranceNo _ [] = 0
getOccuranceNo check (x:xs)
    | check == x = 1 + getOccuranceNo check xs
    | otherwise = getOccuranceNo check xs
