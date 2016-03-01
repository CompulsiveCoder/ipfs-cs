DIR=$PWD

#sudo apt-get update
sudo apt-get install -y mono-complete

cd lib
sh get-libs.sh
cd $DIR
