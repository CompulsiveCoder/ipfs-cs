PRE_COMMIT_FILE=".git/hooks/pre-commit"

if [ -f $PRE_COMMIT_FILE ]; then
    cp "$PRE_COMMIT_FILE" "$PRE_COMMIT_FILE.bak"
fi

echo "#!/bin/sh\nsh test.sh" > $PRE_COMMIT_FILE

chmod +x $PRE_COMMIT_FILE



