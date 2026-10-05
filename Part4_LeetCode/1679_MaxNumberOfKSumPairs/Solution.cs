
int MaxOperations(int[] nums, int k)
{
    int operations = 0;
    // sorting 

    Array.Sort(nums);


    // checking
    int left = 0;
    int right = nums.Length - 1;
    while (left < right)
    {

        if (nums[left] + nums[right] != k)
        {
            if (nums[left] + nums[right] < k)
            {
                left++;
            }
            else
            {
                right--;
            }


        }
        else
        {
            operations++; left++; right--;
        }
    }

    return operations;
}