main = do
    stdin <- getContents
    putStr "hi"

thing :: String -> String
thing x = x