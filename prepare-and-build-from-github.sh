BRANCH=$1

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

echo "Branch: $BRANCH"

curl https://raw.githubusercontent.com/CompulsiveCoder/ipfs-cs/$BRANCH/prepare.sh | sh
curl https://raw.githubusercontent.com/CompulsiveCoder/ipfs-cs/$BRANCH/build-from-github.sh | sh -s $BRANCH
