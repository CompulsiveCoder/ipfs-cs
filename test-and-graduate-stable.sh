sh build.sh && \
sh test-all.sh && \
sh graduate-stable.sh || \
echo "Tests failed. Graduation aborted."
