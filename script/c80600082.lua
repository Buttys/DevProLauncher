-- 	Vampire Duke
function c80600082.initial_effect(c)
	--spsummon
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(80600082,0))
	e1:SetCategory(CATEGORY_SPECIAL_SUMMON)
	e1:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e1:SetCode(EVENT_SUMMON_SUCCESS)
	e1:SetTarget(c80600082.sptg)
	e1:SetOperation(c80600082.spop)
	c:RegisterEffect(e1)
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(80600082,1))
	e2:SetCategory(CATEGORY_TOGRAVE)
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e2:SetCode(EVENT_SPSUMMON_SUCCESS)
	e2:SetCost(c80600082.cost)
	e2:SetTarget(c80600082.tgtg)
	e2:SetOperation(c80600082.tgop)
	c:RegisterEffect(e2)
	local e3=Effect.CreateEffect(c)
	e3:SetType(EFFECT_TYPE_SINGLE)
	e3:SetProperty(EFFECT_FLAG_UNCOPYABLE+EFFECT_FLAG_CANNOT_DISABLE)
	e3:SetCode(EFFECT_CANNOT_BE_XYZ_MATERIAL)
	e3:SetValue(c80600082.xyzlimit)
	c:RegisterEffect(e3)
end
function c80600082.filter(c,e,tp)
	return c:IsSetCard(0x8e) and c:IsAttribute(ATTRIBUTE_DARK) and c:IsCanBeSpecialSummoned(e,0,tp,false,false)
end
function c80600082.sptg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chkc then return chkc:IsLocation(LOCATION_GRAVE) and chkc:IsControler(tp) and c80600082.filter(chkc,e,tp) end
	if chk==0 then return Duel.GetLocationCount(tp,LOCATION_MZONE)>0
		and Duel.IsExistingTarget(c80600082.filter,tp,LOCATION_GRAVE,0,1,nil,e,tp) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_SPSUMMON)
	local g=Duel.SelectTarget(tp,c80600082.filter,tp,LOCATION_GRAVE,0,1,1,nil,e,tp)
	Duel.SetOperationInfo(0,CATEGORY_SPECIAL_SUMMON,g,1,0,0)
end
function c80600082.spop(e,tp,eg,ep,ev,re,r,rp)
	if Duel.GetLocationCount(tp,LOCATION_MZONE)<=0 then return end
	local tc=Duel.GetFirstTarget()
	if tc then 
		Duel.SpecialSummon(tc,0,tp,tp,false,false,POS_FACEUP_DEFENCE)
	end
end
function c80600082.tgtg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return true end
	Duel.Hint(HINT_SELECTMSG,tp,555)
	local op=Duel.SelectOption(tp,70,71,72)
	e:SetLabel(op)
	Duel.SetOperationInfo(0,CATEGORY_TOGRAVE,nil,1,1-tp,LOCATION_DECK)
end
function c80600082.tgfilter(c,ty)
	return c:IsType(ty) and c:IsAbleToGrave()
end
function c80600082.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetFlagEffect(tp,80600082)==0 end
	Duel.RegisterFlagEffect(tp,80600082,RESET_PHASE+PHASE_END,0,1)
end
function c80600082.tgop(e,tp,eg,ep,ev,re,r,rp)
	local g=nil
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_TOGRAVE)
	if e:GetLabel()==0 then g=Duel.SelectMatchingCard(1-tp,c80600082.tgfilter,1-tp,LOCATION_DECK,0,1,1,nil,TYPE_MONSTER)
	elseif e:GetLabel()==1 then g=Duel.SelectMatchingCard(1-tp,c80600082.tgfilter,1-tp,LOCATION_DECK,0,1,1,nil,TYPE_SPELL)
	else g=Duel.SelectMatchingCard(1-tp,c80600082.tgfilter,1-tp,LOCATION_DECK,0,1,1,nil,TYPE_TRAP) end
	if g:GetCount()~=0 then
		Duel.SendtoGrave(g,REASON_EFFECT)
	else
		local cg=Duel.GetFieldGroup(tp,0,LOCATION_DECK)
		Duel.ConfirmCards(tp,cg)
		Duel.ShuffleDeck(1-tp)
	end
end
function c80600082.ftarget(e,c)
	return e:GetLabel()~=c:GetFieldID()
end
function c80600082.xyzlimit(e,c)
	if not c then return false end
	return not c:IsAttribute(ATTRIBUTE_DARK)
end
