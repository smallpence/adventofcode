import java.io.File

fun main(args: Array<String>) {
    val foundRules:MutableList<MutableList<String>> = mutableListOf()

    var currentLines: MutableList<String> = mutableListOf()
    File("input").forEachLine { line ->
        if (line.length == 0) {
            foundRules.add(currentLines)
            currentLines = mutableListOf()
        } else {
            currentLines.add(line)
        }
    }
    foundRules.add(currentLines)

    val combinedRules = foundRules.mapNotNull { ruleList -> ruleList
        .map { rule -> rule.toCharArray().toList() }
        .fold(null) { acc: List<Char>?, list ->
            acc?.filter { char -> list.contains(char) } ?: list
         }
//        .flatten()
//        .distinct()
    }
        .map { answers -> answers.size }
        .sum()

    println(foundRules)
    println(combinedRules)
}