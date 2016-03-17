BRANCH=$1
TEST_CATEGORY=$2

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

if [ -z "$TEST_CATEGORY" ]; then
    TEST_CATEGORY="Unit"
fi
echo "Branch: $BRANCH"
echo "Tests: $TEST_CATEGORY"

git clone https://github.com/CompulsiveCoder/ipfs-cs.git --branch $BRANCH
cd ipfs-cs
sh init-build-test.sh $TEST_CATEGORY
