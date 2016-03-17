TEST_CATEGORY=$1

if [ -z "$TEST_CATEGORY" ]; then
    TEST_CATEGORY="Unit"
fi

echo "Tests: $TEST_CATEGORY"

docker run -i -v $PWD:/ipfs-cs compulsivecoder/ubuntu-mono-ipfs /bin/bash -c "cp /ipfs-cs /ipfs-cs-staging -r && cd /ipfs-cs-staging && sh test.sh $TEST_CATEGORY"
