#sudo apt-get update
sudo apt-get install -y mono-complete

DIR=$PWD

cd lib
sh get-libs.sh
cd $DIR
