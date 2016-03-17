#!/bin/bash
BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')

if [ "$BRANCH" = "master" ]
then
  echo "Graduating to stable branch"

  # Checkout the master branch
  git checkout stable && \

  # Ensure it's up to date
  git pull origin stable && \

  # Merge the dev branch
  git merge master && \

  # Push the updates
  git push origin stable && \

  # Go back to the dev branch
  git checkout master && \

  echo "The 'master' branch has been graduated to the 'stable' branch" || \

  echo "Error"
else
  echo "You must be in the 'master' branch to graduate to the 'stable' branch."
fi
date
