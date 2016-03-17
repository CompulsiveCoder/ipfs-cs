echo "Starting build for ipfs-cs project"
echo "Dir: $PWD"

MODE=$1

if [ -z "$MODE" ]; then
    MODE="Release"
fi

echo "Mode: $MODE"

xbuild src/ipfs-cs.sln /p:Configuration=$MODE
