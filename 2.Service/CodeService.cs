using System.Collections;
using System.Collections.Generic;
using MyProject.Domain;
using MyProject.Infrastructure;
using MyProject.Repository;
namespace MyProject.Service
{
    public class CodeService : ICodesService
    {
        private IValidationDictionary _validationDictionary;
        private ICodeSalesRepository _repository;
        public CodeService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new CodeSalesRepository())
        { }
        public CodeService(IValidationDictionary validationDictionary, ICodeSalesRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }
        private bool ValidateCode(MaKhuyenMai codetoValidate)
        {
            _validationDictionary.Clear();
            if (codetoValidate.MaKM.Length == 0)
                _validationDictionary.AddError("MaKM", "Vui lòng nhập mã khuyến mãi");
            if (codetoValidate.TiLe < 0 || codetoValidate.TiLe > 100)
                _validationDictionary.AddError("TiLe", "Vui lòng nhập tỉ lệ hợp lệ");
            return _validationDictionary.IsValid;
        }
        public bool CreateCode(MaKhuyenMai Target)
        {
            if (!ValidateCode(Target))
                return false;
            try
            {
                _repository.CreateCode(Target);
            }
            catch {
                return false;
            }
            return true;
        }

        public bool DeleteCode(MaKhuyenMai Target)
        {
            try
            {
                _repository.DeleteCode(Target);

            }
            catch
            { return false;
            }
            return true;
        }

        public MaKhuyenMai getCodes(string key)
        {
            return _repository.getCodes(key);
        }
        IEnumerable listSearch;
        public IEnumerable listCodes()
        {
            return listSearch=_repository.listCodes();
        }
        public IEnumerable searchCodes(string key)
        {
            List<MaKhuyenMai> result = new List<MaKhuyenMai>();
            foreach (MaKhuyenMai item in listSearch)
            {
                if (item.MaKM.Contains(key))
                    result.Add(item);
            }
            return result;
        }
        public bool UpdateCode(MaKhuyenMai Target)
        {
            if (!ValidateCode(Target))
                return false;
            try
            {
                _repository.UpdateCode(Target);
            }
            catch 
            {
                return false;
            }
            return true;
        }
    }
}
