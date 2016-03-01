#sudo apt-get update
sudo apt-get install -y mono-runtime

DIR=$PWD

cd lib
sh get-libs.sh
cd $DIR
