CATEGORY=$1

if [ -z "$CATEGORY" ]; then
    CATEGORY="Unit"
fi

echo "Category: $CATEGORY"

sh build.sh && \
sh test.sh $CATEGORY
