echo "Preparing for ipfs-cs project"
echo "Dir: $PWD"

sudo apt-get update
sudo apt-get install -y git wget mono-complete screen

mozroots --import --sync
