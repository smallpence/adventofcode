import re
from dataclasses import dataclass
from functools import reduce
from typing import List, Tuple, Set


@dataclass
class Bag:
    type: str
    has: List[str]


def parse_bag_colour(bag: str) -> str:
    return re.search(r'\d? ?(.+) bag', bag).groups("")[0]


def line_to_bag(line: str) -> Bag:
    found_pattens = re.search(r'(.+) bags contain (.+)', line)
    bag_type = found_pattens.groups("")[0]
    bag_has = map(parse_bag_colour, found_pattens.groups("")[1].split(", "))

    return Bag(bag_type, list(bag_has))


def get_containing_bags(acc: Tuple[str, List[str]], val: Bag) -> Tuple[str, List[str]]:
    bag_type, containing_bag_types = acc
    if bag_type in val.has:
        containing_bag_types.append(val.type)

    return bag_type, containing_bag_types


if __name__ == '__main__':
    file = open("input", "r")
    trimmed_lines = map(str.strip, file.readlines())
    bags = list(map(line_to_bag, trimmed_lines))
    gold_containing_types = ["shiny gold"]
    total_found_bag_types:Set[str] = set()

    while gold_containing_types:
        check_bag_type = gold_containing_types.pop(0)
        found_bag_types = reduce(get_containing_bags, bags, (check_bag_type, list()))[1]
        gold_containing_types.extend(found_bag_types)
        total_found_bag_types.update(found_bag_types)

    print(len(total_found_bag_types))
