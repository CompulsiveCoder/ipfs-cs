sh build.sh

CATEGORY=$1

if [ -z "$CATEGORY" ]; then
    CATEGORY="Unit"
fi

echo "Category: $CATEGORY"

mono lib/NUnit.Runners.2.6.4/tools/nunit-console.exe bin/Release/*.dll --include=$CATEGORY
