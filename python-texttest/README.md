RPG Combat Kata in Python with TextTest
========================================

Problem description: [RPG Combat Kata](https://sammancoaching.org/kata_descriptions/rpg_combat.html). This folder has a starting position for Python and the approval testing tool [TextTest](https://texttest.org).

This branch has a sample solution set of tests using TextTest. There are actually three equivalent test suites each demonstrating a different style of test design:

* Features - tests organized by feature
* ManySmallTests - fine-grained tests for one small feature at a time
* OneBigTest - one big battle with lots of moves.

Understanding tradeoffs in test design
--------------------------------------

This exercise aims to illustrate the consequences of the three different styles of test design included in this test suite.

Under 'src' there are several copies of the production code. The 'rpg_combat.py' file is my best effort at a bug-free solution. The others, labelled 'bug1', 'bug2' ..., are copies of it each with a different bug deliberately inserted. The idea is to discover which style(s) of test design make it easiest to interpret test failures.

* In 'test_rig.py' change the import statement to something like:

    import rpg_combat_bug1 as rpg_combat

* Pick one of the three test suites and run the tests. At least one test should fail because of the bug. 
* How easy is it to work out what the bug is just from the failure information? 
* Run the other two test suites in turn. Is it easier or harder to work out what the bug is from these other failures?
* Do a comparision of the buggy code with the bug-free code to confirm your suspicions about what the bug is.
* Repeat for the other bugs - change the import statement in 'test_rig.py'
* Can you draw any conclusions about which style of tests makes it easier to interpret failures?
* Refactor existing tests and/or design new ones that you think will be even easier to interpret on failure.
