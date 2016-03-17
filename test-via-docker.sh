TEST_CATEGORY=$1

if [ -z "$TEST_CATEGORY" ]; then
    TEST_CATEGORY="Unit"
fi

echo "Tests: $TEST_CATEGORY"

sh build.sh

docker run -i -v /var/run/docker.sock:/var/run/docker.sock -v $PWD:/ipfs-cs compulsivecoder/ubuntu-mono-ipfs /bin/bash -c "cd /ipfs-cs && sh test.sh $TEST_CATEGORY"
