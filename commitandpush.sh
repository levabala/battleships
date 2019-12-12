branch=${2:-master}

git add ./
git commit -m "$1"
git push origin "$branch"