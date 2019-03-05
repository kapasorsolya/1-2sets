#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

using namespace std;

void print(int*, int);
int isPromising(int*, int, int);
int isSolution(int*, int, int);
void BT(int*, int, int, int);

int counter = 0;
bool firstSolution = false;

int main(int argc,char*argv[])
{
	int n = 9; // n- domenium milyen 
	int p = 6; // p elembol all
	int*a = (int*)malloc(n * sizeof(int));
	if (a == NULL)
	{
		cout << "unsuccessful reservation";
		return 0;
	}

	BT(a, n, 0, p);
	cout << "Backrack: number of assignment = " << counter << endl;

	cout << "Press any key...."<<endl;
	cin.get();

	free(a);
	return 0;
}


void BT(int*x, int n, int k, int p)
{
	for (x[k] = 1;x[k] <= n;x[k]++)
	{
		if (firstSolution == false)
		{
			if (isPromising(x, n, k))
			{
				counter++;
				if (isSolution(x, k, p))
				{
					print(x, p);

				}
				else
				{
					BT(x, n, k + 1, p);
				}
			}
		}
		
	}
}



int isSolution(int*x, int k, int p)
{
	if (k < p - 1)
	{
		return 0;
	}
	else
	{
		firstSolution = true;
		return 1;
	}
}

int isPromising(int*x, int p, int k)
{
	if (k==5)
	{
		if (x[k] != x[k - 1] * 2) {
			return false;
		}
	}

	int i;
	for (i = 0;i <= k - 1;i++)
	{
		if (x[i] == x[k] )
		{
			return false;
		}
	}
	return true;
}

void print(int*x, int n)
{
	int i;
	for (i = 0;i < n;i++)
	{
		printf("%d ", x[i]);
	}
	printf("\n");

}
