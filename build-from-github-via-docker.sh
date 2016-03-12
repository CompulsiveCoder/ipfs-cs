BRANCH=$1

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

docker run -it compulsivecoder/ubuntu-mono /bin/bash -c "curl https://raw.githubusercontent.com/CompulsiveCoder/ipfs-cs/$BRANCH/build-from-github.sh | sh -s $BRANCH"
